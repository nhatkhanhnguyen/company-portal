using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Products.Commands;

public record UpdateProductCommand(ProductViewModel Product) : IRequest<bool>
{
    public class Handler(IMapper mapper, IRepository<Product> repository, IUnitOfWork uow)
        : IRequestHandler<UpdateProductCommand, bool>
    {
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Product>(request.Product);
            repository.Update(entity);
            return await uow.SaveChangesAsync();
        }
    }
}