using Azure.Storage.Blobs;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Products.Commands;

public record DeleteProductResourcesFromStorageCommand(int ProductId) : IRequest
{
    public class Handler(IRepository<Resource> repository, BlobServiceClient blobServiceClient) : IRequestHandler<DeleteProductResourcesFromStorageCommand>
    {
        public async Task Handle(DeleteProductResourcesFromStorageCommand request, CancellationToken cancellationToken)
        {
            var blobNames = await repository.Query(x => x.ProductId == request.ProductId).Select(x => x.BlobName).ToListAsync(cancellationToken);
            var containerClient = blobServiceClient.GetBlobContainerClient("product-image");
            foreach (var blobClient in blobNames.Select(blobName => containerClient.GetBlobClient(blobName)))
            {
                await blobClient.DeleteAsync(cancellationToken: cancellationToken);
            }
        }
    }
}