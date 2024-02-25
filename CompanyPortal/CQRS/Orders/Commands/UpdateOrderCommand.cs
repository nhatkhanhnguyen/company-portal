using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Commands;

public record UpdateOrderCommand(OrderViewModel Order) : IRequest<bool>
{

    public class Handler(IMapper mapper, IRepository<Order> repository, IUnitOfWork uow)
        : IRequestHandler<UpdateOrderCommand, bool>
    {
        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await repository.GetAsync(request.Order.Id, cancellationToken);
            if (order == null)
            {
                return false;
            }

            mapper.Map(request.Order, order);
            repository.Update(order);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}