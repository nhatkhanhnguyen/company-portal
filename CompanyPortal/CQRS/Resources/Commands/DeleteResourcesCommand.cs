using Azure.Storage.Blobs;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Resources.Commands;

public record DeleteResourcesCommand(List<int> ResourceIds, bool ForceDelete = false) : IRequest<Result>
{
    public class Handler(IRepository<Resource> repository, BlobServiceClient blobServiceClient, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<DeleteResourcesCommand, Result>
    {
        public async Task<Result> Handle(DeleteResourcesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blobs = await repository
                    .Query(x => request.ResourceIds.Contains(x.Id))
                    .Select(x => new
                    {
                        Name = x.BlobName,
                        Container = GetBlobContainer(x)
                    })
                    .ToListAsync(cancellationToken);

                repository.Delete(x => request.ResourceIds.Contains(x.Id), request.ForceDelete);
                var result = await uow.SaveChangesAsync(cancellationToken);

                if (result && request.ForceDelete)
                {
                    foreach (var blobClient in from blob in blobs let containerClient = blobServiceClient.GetBlobContainerClient(blob.Container) select containerClient.GetBlobClient(blob.Name))
                    {
                        await blobClient.DeleteAsync(cancellationToken: cancellationToken);
                    }
                }
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error(false);
            }
        }

        private string GetBlobContainer(Resource resource)
        {
            return resource.ArticleId != null
                ? "article-image"
                : resource.CategoryId != null
                    ? "category-image"
                    : "product-image";
        }
    }
}
