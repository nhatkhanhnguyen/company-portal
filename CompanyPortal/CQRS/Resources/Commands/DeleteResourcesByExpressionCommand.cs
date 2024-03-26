using System.Linq.Expressions;
using Azure.Storage.Blobs;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Resources.Commands;

public record DeleteResourcesByExpressionCommand(Expression<Func<Resource, bool>> Expression, bool ForceDelete) : IRequest<Result>
{
    public class Handler(IRepository<Resource> repository, BlobServiceClient blobServiceClient, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<DeleteResourcesByExpressionCommand, Result>
    {
        public async Task<Result> Handle(DeleteResourcesByExpressionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<(int? ArticleId, int? CategoryId, int? ProductId, string BlobName)> blobs = await repository
                    .Query(request.Expression)
                    .Select(x => new ValueTuple<int?, int?, int?, string>(
                        x.ArticleId, x.CategoryId, x.ProductId, x.BlobName)
                    ).ToListAsync(cancellationToken);

                repository.Delete(request.Expression, request.ForceDelete);
                var result =  await uow.SaveChangesAsync(cancellationToken);

                if (result && request.ForceDelete)
                {
                    foreach (var blob in blobs)
                    {
                        var containerClient = blobServiceClient.GetBlobContainerClient(GetBlobContainer(blob));
                        var blobClient = containerClient.GetBlobClient(blob.BlobName);
                        await blobClient.DeleteAsync(cancellationToken: cancellationToken);
                    }
                }

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang xóa tài nguyên khỏi CSDL.");
            }
        }

        private string GetBlobContainer((int? ArticleId, int? CategoryId, int? ProductId, string BlobName) resource)
        {
            if (resource.ArticleId != null) return "article-image";
            if (resource.CategoryId != null) return "category-image";
            return resource.ProductId != null ? "product-image" : string.Empty;
        }
    }
}
