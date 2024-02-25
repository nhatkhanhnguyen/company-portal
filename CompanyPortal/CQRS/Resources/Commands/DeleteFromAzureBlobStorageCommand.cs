using Azure.Storage.Blobs;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record DeleteFromAzureBlobStorageCommand(string BlobName) : IRequest<bool>
{
    public class Handler(BlobServiceClient blobServiceClient)
        : IRequestHandler<DeleteFromAzureBlobStorageCommand, bool>
    {
        public async Task<bool> Handle(DeleteFromAzureBlobStorageCommand request, CancellationToken cancellationToken)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("product-image");
            var blobClient = containerClient.GetBlobClient(request.BlobName);
            var result = await blobClient.DeleteAsync(cancellationToken: cancellationToken);
            return !result.IsError;
        }
    }
}