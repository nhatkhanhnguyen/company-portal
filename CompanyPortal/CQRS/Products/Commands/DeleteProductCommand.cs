using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.Products.Commands;

public record DeleteProductCommand(int ProductId, bool ForceDelete = false) : IRequest<bool>
{
    public class Handler(IRepository<Product> productRepository, IRepository<Resource> resourceRepository, IUnitOfWork uow)
        : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            resourceRepository.Delete(x => x.ProductId == request.ProductId, request.ForceDelete);
            productRepository.Delete(x => x.Id == request.ProductId, request.ForceDelete);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}