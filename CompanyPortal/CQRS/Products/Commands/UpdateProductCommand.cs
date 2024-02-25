using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Products.Commands;

public record UpdateProductCommand(ProductViewModel Product) : IRequest<int>
{
    public class Handler(IMapper mapper, IRepository<Product> repository, IUnitOfWork uow)
        : IRequestHandler<UpdateProductCommand, int>
    {
        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await repository.GetAsync(request.Product.Id, cancellationToken);
            if (product == null)
            {
                return 0;
            }

            mapper.Map(request.Product, product);
            repository.Update(product);
            await uow.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}