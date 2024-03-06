using Azure.Storage.Blobs;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Resources.Commands;

public record DeleteReferencingResourcesCommand(int ProductId, bool ForceDelete) : IRequest<Result>
{
    public class Handler(IRepository<Resource> repository, IUnitOfWork uow,
        BlobServiceClient blobServiceClient, ILogger<Handler> logger) : IRequestHandler<DeleteReferencingResourcesCommand, Result>
    {
        public async Task<Result> Handle(DeleteReferencingResourcesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blobNames = await repository.Query(x => x.ProductId == request.ProductId).Select(x => x.BlobName).ToListAsync();
                repository.Delete(x => x.ProductId == request.ProductId, request.ForceDelete);
                var result = await uow.SaveChangesAsync(cancellationToken);
                if (request.ForceDelete)
                {
                    var containerClient = blobServiceClient.GetBlobContainerClient("product-image");
                    foreach (var blobName in blobNames)
                    {
                        var blobClient = containerClient.GetBlobClient(blobName);
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
    }
}
