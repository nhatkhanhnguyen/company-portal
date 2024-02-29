using Azure.Storage.Blobs;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Articles.Commands;

public record DeleteArticleCommand(int ArticleId, bool ForceDelete = false) : IRequest<bool>
{
    public class Handler(IRepository<Article> articleRepository, IRepository<Resource> resourceRepository, BlobServiceClient blobServiceClient, IUnitOfWork uow) : IRequestHandler<DeleteArticleCommand, bool>
    {
        public async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var blobNames = new List<string>();
            if (request.ForceDelete) {
                blobNames = await resourceRepository
                    .Query(x => x.ArticleId == request.ArticleId)
                    .Select(x => x.BlobName)
                    .ToListAsync(cancellationToken);
            }

            articleRepository.Delete(x => x.Id == request.ArticleId, request.ForceDelete);
            resourceRepository.Delete(x => x.ArticleId == request.ArticleId, request.ForceDelete);

            var result = await uow.SaveChangesAsync(cancellationToken);
            if (result && request.ForceDelete)
            {
                await DeleteFromStorageAsync(blobNames, cancellationToken);
            }

            return result;
        }

        private async Task DeleteFromStorageAsync(IEnumerable<string> blobNames, CancellationToken cancellationToken = default)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("product-image");
            foreach (var blobClient in blobNames.Select(blobName => containerClient.GetBlobClient(blobName)))
            {
                await blobClient.DeleteAsync(cancellationToken: cancellationToken);
            }
        }
    }
}