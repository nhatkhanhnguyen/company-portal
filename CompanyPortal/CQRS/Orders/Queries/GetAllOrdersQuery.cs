using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Orders.Queries;

public record GetAllOrdersQuery(bool ForceRefresh) : ICachedQuery<List<OrderViewModel>>
{
    public class Handler(IRepository<Order> repository, IMapper mapper)
        : IRequestHandler<GetAllOrdersQuery, List<OrderViewModel>>
    {
        public async Task<List<OrderViewModel>> Handle(GetAllOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAll()
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Images)
                .Select(x => mapper.Map<OrderViewModel>(x))
                .ToListAsync(cancellationToken);
            return mapper.Map<List<OrderViewModel>>(result);
        }
    }

    public string Key => "orders";

    public TimeSpan? Expiration => null;
}