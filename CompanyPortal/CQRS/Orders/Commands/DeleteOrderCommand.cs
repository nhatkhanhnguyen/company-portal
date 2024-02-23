using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Commands;

public record DeleteOrderCommand(int OrderId, bool ForceDelete = false) : IRequest<bool>
{
    public class Handler(IRepository<Product> repository, IUnitOfWork uow)
        : IRequestHandler<DeleteOrderCommand, bool>
    {
        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.OrderId, request.ForceDelete, cancellationToken);
            return await uow.SaveChangesAsync();
        }
    }
}