using Azure.Storage.Blobs;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record DeleteFromStorageCommand(IEnumerable<string> BlobNames) : IRequest
{
    public class Handler(BlobServiceClient blobServiceClient, ILogger<Handler> logger)
        : IRequestHandler<DeleteFromStorageCommand>
    {
        public async Task Handle(DeleteFromStorageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var containerClient = blobServiceClient.GetBlobContainerClient("product-image");
                foreach (var blobName in request.BlobNames)
                {
                    var blobClient = containerClient.GetBlobClient(blobName);
                    await blobClient.DeleteAsync(cancellationToken: cancellationToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, request.BlobNames);
            }
        }
    }
}