using Azure.Storage.Blobs;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Categories.Commands;

public record DeleteCategoryCommand(int CategoryId, bool ForceDelete = false) : IRequest<Result>
{
    public class Handler(IRepository<Category> categoryRepository, IRepository<Resource> resourceRepository, IRepository<Product> productRepository, 
        BlobServiceClient blobServiceClient, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<DeleteCategoryCommand, Result>
    {
        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blobNames = new List<string>();
                if (request.ForceDelete)
                {
                    blobNames = await resourceRepository
                        .Query(x => x.CategoryId == request.CategoryId)
                        .Select(x => x.BlobName)
                        .ToListAsync(cancellationToken);
                }

                resourceRepository.Delete(x => x.CategoryId == request.CategoryId, request.ForceDelete);
                categoryRepository.Delete(x => x.Id == request.CategoryId, request.ForceDelete);
                var result = await uow.SaveChangesAsync(cancellationToken);

                if (result && request.ForceDelete)
                {
                    await DeleteFromStorageAsync(blobNames, cancellationToken);
                    productRepository.Delete(x => x.CategoryId == request.CategoryId, request.ForceDelete);
                }

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang xóa danh mục khỏi CSDL.");
            }
        }

        private async Task DeleteFromStorageAsync(IEnumerable<string> blobNames, CancellationToken cancellationToken = default)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("category-image");
            foreach (var blob in blobNames)
            {
                var blobClient = containerClient.GetBlobClient(blob);
                await blobClient.DeleteAsync(cancellationToken: cancellationToken);
            }
        }
    }
}