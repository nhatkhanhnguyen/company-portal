using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Categories.Commands;

public record UncategoriseProductCommand(int ProductId) : IRequest<Result>
{
    public class Handler(IRepository<Product> repository, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<UncategoriseProductCommand, Result>
    {
        public async Task<Result> Handle(UncategoriseProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await repository.Query(x => x.Id == request.ProductId).FirstOrDefaultAsync(cancellationToken);
                if (product == null)
                {
                    logger.LogError("Product with {Id} not found.", request.ProductId);
                    return Result.Error($"Sản phẩm có ID = {request.ProductId} không tồn tại khi đang tiến hành lưu vào CSDL.");
                }

                product.CategoryId = null;
                repository.Update(product);
                var result = await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error<string>("Có lỗi xảy ra khi đang lưu sản phẩm vào CSDL.");
            }
        }
    }
}