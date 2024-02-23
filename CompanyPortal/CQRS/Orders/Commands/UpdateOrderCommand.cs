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
            var entity = mapper.Map<Order>(request.Order);
            repository.Update(entity);
            return await uow.SaveChangesAsync();
        }
    }
}