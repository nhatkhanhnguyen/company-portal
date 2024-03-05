using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Products.Commands;

public record UpdateProductCommand(ProductViewModel Product) : IRequest<Result>
{
    public class Handler(
        IRepository<Product> productRepository, IRepository<Resource> resourceRepository, IUnitOfWork uow,
        IMapper mapper, ILogger<Handler> logger) : IRequestHandler<UpdateProductCommand, Result>
    {
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetAsync(request.Product.Id, cancellationToken);
            if (product == null)
            {
                logger.LogError("Product with {Id} not found.", request.Product.Id);
                return Result.Error($"Sản phẩm có ID = {request.Product.Id} không tồn tại khi đang tiến hành lưu vào CSDL.");
            }

            try
            {
                mapper.Map(request.Product, product);
                productRepository.Update(product);
                if (product.IsActive)
                {
                    resourceRepository.Undelete(x => x.ProductId == request.Product.Id);
                }
                else
                {
                    resourceRepository.Delete(x => x.ProductId == request.Product.Id);
                }

                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(product.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error<string>("Có lỗi xảy ra khi đang lưu sản phẩm vào CSDL.");
            }
        }
    }
}