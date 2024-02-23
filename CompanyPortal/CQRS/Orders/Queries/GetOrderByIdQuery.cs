using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Queries;

public record GetOrderByIdQuery(int OrderId) : IRequest<OrderViewModel>
{
    public class Handler(IRepository<Order> repository, IMapper mapper)
        : IRequestHandler<GetOrderByIdQuery, OrderViewModel>
    {
        public async Task<OrderViewModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FirstOrDefaultAsync(request.OrderId, cancellationToken);
            return mapper.Map<OrderViewModel>(result);
        }
    }
}

