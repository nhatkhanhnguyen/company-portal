using Azure.Storage.Blobs;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Articles.Commands;

public record DeleteArticleCommand(int ArticleId, bool ForceDelete) : IRequest<Result>
{
    public class Handler(IRepository<Article> articleRepository, IRepository<Resource> resourceRepository, 
        BlobServiceClient blobServiceClient, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<DeleteArticleCommand, Result>
    {
        public async Task<Result> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blobNames = new List<string>();
                if (request.ForceDelete)
                {
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

                return Result.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi xóa bài viết khỏi CSDL. Vui lòng thử lại sau!");
            }
        }

        private async Task DeleteFromStorageAsync(IEnumerable<string> blobNames, CancellationToken cancellationToken = default)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("article-image");
            foreach (var blobClient in blobNames.Select(blobName => containerClient.GetBlobClient(blobName)))
            {
                await blobClient.DeleteAsync(cancellationToken: cancellationToken);
            }
        }
    }
}