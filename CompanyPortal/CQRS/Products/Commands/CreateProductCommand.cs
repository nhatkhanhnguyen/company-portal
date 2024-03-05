using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Products.Commands;

public record CreateProductCommand(ProductViewModel Product) : IRequest<Result>
{
    public class Handler(IRepository<Product> repository, IUnitOfWork uow,
        IMapper mapper, ILogger<Handler> logger) : IRequestHandler<CreateProductCommand, Result>
    {
        public async Task<Result> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            try 
            {
                var entity = mapper.Map<Product>(request.Product);
                await repository.InsertAsync(entity, cancellationToken);
                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(entity.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error<string>("Có lỗi xảy ra khi đang lưu sản phẩm vào CSDL.");
            }
        }
    }
}