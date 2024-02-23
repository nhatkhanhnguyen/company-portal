using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Commands;

public record CreateOrderCommand(OrderViewModel Order) : IRequest<bool>
{
    public class Handler(IMapper mapper, IRepository<Order> repository, IUnitOfWork uow)
        : IRequestHandler<CreateOrderCommand, bool>
    {
        public async Task<bool> Handle(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Order>(request.Order);
            await repository.InsertAsync(entity, cancellationToken);
            return await uow.SaveChangesAsync();
        }
    }
}