using Azure.Storage.Blobs;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record DeleteFromAzureBlobStorageCommand(string BlobId) : IRequest<bool>
{
    public class Handler(BlobContainerClient client, ILogger<DeleteFromAzureBlobStorageCommand> logger) : IRequestHandler<DeleteFromAzureBlobStorageCommand, bool>
    {
        public async Task<bool> Handle(DeleteFromAzureBlobStorageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blob = client.GetBlobClient(request.BlobId);
                var result = await blob.DeleteAsync(cancellationToken: cancellationToken);
                return !result.IsError;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
