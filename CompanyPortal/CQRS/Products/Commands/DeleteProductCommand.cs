using Azure.Storage.Blobs;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Products.Commands;

public record DeleteProductCommand(int ProductId, bool ForceDelete) : IRequest<bool>
{
    public class Handler(IRepository<Product> productRepository, IRepository<Resource> resourceRepository, BlobServiceClient blobServiceClient, IUnitOfWork uow)
        : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var blobNames = new List<string>();
            if (request.ForceDelete) {
                blobNames = await resourceRepository
                    .Query(x => x.ProductId == request.ProductId)
                    .Select(x => x.BlobName)
                    .ToListAsync(cancellationToken);
            }

            productRepository.Delete(x => x.Id == request.ProductId, request.ForceDelete);
            resourceRepository.Delete(x => x.ProductId == request.ProductId, request.ForceDelete);
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