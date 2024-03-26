using Azure.Storage.Blobs;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record DeleteResourceCommand(int ResourceId, bool ForceDelete = false) : IRequest<Result>
{
    public class Handler(IRepository<Resource> repository, BlobServiceClient blobServiceClient, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<DeleteResourceCommand, Result>
    {
        public async Task<Result> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resource = await repository.FirstOrDefaultAsync(x => x.Id == request.ResourceId, cancellationToken);

                if (resource == null)
                {
                    logger.LogError("Resource with {Id} not found.", request.ResourceId);
                    return Result.Error($"Tài nguyên có ID = {request.ResourceId} không tồn tại khi đang tiến hành đọc từ CSDL.");
                }

                var blobContainer = GetBlobContainer(resource);

                await repository.DeleteByIdAsync(request.ResourceId, request.ForceDelete, cancellationToken);
                var result = await uow.SaveChangesAsync(cancellationToken);

                if (!(result && request.ForceDelete))
                {
                    return Result.Ok(result);
                }

                var containerClient = blobServiceClient.GetBlobContainerClient(blobContainer);
                var blobClient = containerClient.GetBlobClient(resource.BlobName);
                await blobClient.DeleteAsync(cancellationToken: cancellationToken);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang xóa tài nguyên khỏi CSDL.");
            }
        }

        private string GetBlobContainer(Resource resource)
        {
            return resource.ArticleId != null
                ? "article-image"
                : resource.CategoryId != null
                    ? "category-image"
                    : "product-image";
        }
    }
}