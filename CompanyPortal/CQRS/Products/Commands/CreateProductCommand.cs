using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Products.Commands;

public record CreateProductCommand(ProductViewModel Product) : IRequest<int>
{
    public class Handler(IMapper mapper, IRepository<Product> repository, IUnitOfWork uow)
        : IRequestHandler<CreateProductCommand, int>
    {
        public async Task<int> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Product>(request.Product);
            await repository.InsertAsync(entity, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}