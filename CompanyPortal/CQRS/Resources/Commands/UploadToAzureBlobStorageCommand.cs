using Azure.Storage.Blobs;
using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record UploadToAzureBlobStorageCommand(Syncfusion.Blazor.Inputs.FileInfo File) : IRequest<string>
{
    public class Handler(BlobContainerClient client, ILogger<UploadToAzureBlobStorageCommand> logger) : IRequestHandler<UploadToAzureBlobStorageCommand, string>
    {
        public async Task<string> Handle(UploadToAzureBlobStorageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                BlobClient blobClient = client.GetBlobClient(request.File.Name);
                FileStream fileStream = request.File.
                await blobClient.UploadAsync(fileStream, true);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
