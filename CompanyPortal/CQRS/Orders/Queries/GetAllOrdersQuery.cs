using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Queries;

public record GetAllQueryOrders : IRequest<IEnumerable<OrderViewModel>>
{
    public class Handler(IRepository<Order> repository, IMapper mapper)
        : IRequestHandler<GetAllQueryOrders, IEnumerable<OrderViewModel>>
    {
        public async Task<IEnumerable<OrderViewModel>> Handle(GetAllQueryOrders request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAllListAsync(cancellationToken);
            return mapper.Map<List<OrderViewModel>>(result);
        }
    }
}