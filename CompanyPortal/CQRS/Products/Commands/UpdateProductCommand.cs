using AutoMapper;

using Azure.Storage.Blobs;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Products.Commands;

public record UpdateProductCommand(ProductViewModel Product) : IRequest<int>
{
    public class Handler(
        IMapper mapper, IRepository<Product> productRepository, 
        IRepository<Resource> resourceRepository, IUnitOfWork uow) : IRequestHandler<UpdateProductCommand, int>
    {
        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetAsync(request.Product.Id, cancellationToken);
            if (product == null)
            {
                return 0;
            }

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
            return product.Id;
        }
    }
}