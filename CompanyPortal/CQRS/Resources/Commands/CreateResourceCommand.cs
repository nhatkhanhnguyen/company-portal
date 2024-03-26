using AutoMapper;

using Azure.Storage.Blobs;
using Azure.Storage.Sas;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record CreateResourceCommand(ResourceViewModel Resource) : IRequest<Result>
{
    public class Handler(IMapper mapper, IRepository<Resource> repository, BlobServiceClient blobServiceClient,
        IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<CreateResourceCommand, Result>
    {
        public async Task<Result> Handle(CreateResourceCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = mapper.Map<Resource>(request.Resource);
                var containerClient = blobServiceClient.GetBlobContainerClient(GetBlobContainer(request.Resource));
                var blobName = $"{Guid.NewGuid()}.{request.Resource.Name.Split('.')[1]}";
                var blobClient = containerClient.GetBlobClient(blobName);

                var stream = new MemoryStream(Convert.FromBase64String(request.Resource.Base64Content.Split(',')[1]));
                await blobClient.UploadAsync(stream, true, cancellationToken);
                stream.Close();

                entity.Url = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddDays(99999)).ToString();
                entity.BlobName = blobName;
                await repository.InsertAsync(entity, cancellationToken);
                var result = await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(entity.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang tạo tài nguyên.");
            }
        }

        private string GetBlobContainer(ResourceViewModel resource)
        {
            return resource.ArticleId != null
                ? "article-image"
                : resource.CategoryId != null
                    ? "category-image"
                    : "product-image";
        }
    }
}