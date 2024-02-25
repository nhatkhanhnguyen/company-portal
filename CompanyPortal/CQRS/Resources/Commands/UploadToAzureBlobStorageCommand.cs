using Azure.Storage.Blobs;
using Azure.Storage.Sas;

using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record UploadToAzureBlobStorageCommand(ResourceViewModel Resource) : IRequest<(string Url, string BlobName)>
{
    public class Handler(BlobServiceClient blobServiceClient)
        : IRequestHandler<UploadToAzureBlobStorageCommand, (string Url, string BlobName)>
    {
        public async Task<(string Url, string BlobName)> Handle(UploadToAzureBlobStorageCommand request, CancellationToken cancellationToken)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("product-image");
            var blobName = $"{Guid.NewGuid()}.{request.Resource.Name.Split('.')[1]}";
            var blobClient = containerClient.GetBlobClient(blobName);

            var stream = new MemoryStream(Convert.FromBase64String(request.Resource.Base64Content.Split(',')[1]));
            await blobClient.UploadAsync(stream, true, cancellationToken);
            stream.Close();

            return (blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddDays(1)).ToString(),
                blobName);
        }
    }
}