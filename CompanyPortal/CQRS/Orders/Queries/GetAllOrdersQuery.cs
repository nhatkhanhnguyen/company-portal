using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Queries;

public record GetAllOrdersQuery(bool ForceRefresh) : ICachedQuery<List<OrderViewModel>>
{
    public class Handler(IRepository<Order> repository, IMapper mapper)
        : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderViewModel>>
    {
        public async Task<IEnumerable<OrderViewModel>> Handle(GetAllOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAllListAsync(cancellationToken);
            return mapper.Map<List<OrderViewModel>>(result);
        }
    }

    public string Key => "orders";

    public TimeSpan? Expiration => null;
}