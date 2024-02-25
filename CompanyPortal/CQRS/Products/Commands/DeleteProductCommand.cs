using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.Products.Commands;

public record DeleteProductCommand(int ProductId, bool ForceDelete = false) : IRequest<bool>
{
    public class Handler(IRepository<Product> repository, IUnitOfWork uow)
        : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.ProductId, request.ForceDelete, cancellationToken);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}