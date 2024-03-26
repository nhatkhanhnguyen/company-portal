using Azure.Storage.Blobs;
using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Products.Commands;

public record DeleteProductCommand(int ProductId, bool ForceDelete) : IRequest<Result>
{
    public class Handler(
        IRepository<Product> productRepository, IRepository<Resource> resourceRepository, 
        BlobServiceClient blobServiceClient, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<DeleteProductCommand, Result>
    {
        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blobNames = new List<string>();
                if (request.ForceDelete)
                {
                    blobNames = await resourceRepository
                        .Query(x => x.ProductId == request.ProductId)
                        .Select(x => x.BlobName)
                        .ToListAsync(cancellationToken);
                }

                resourceRepository.Delete(x => x.ProductId == request.ProductId, request.ForceDelete);
                productRepository.Delete(x => x.Id == request.ProductId, request.ForceDelete);
                var result = await uow.SaveChangesAsync(cancellationToken);

                if (result && request.ForceDelete)
                {
                    await DeleteFromStorageAsync(blobNames, cancellationToken);
                }

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang xóa sản phẩm khỏi CSDL.");
            }
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