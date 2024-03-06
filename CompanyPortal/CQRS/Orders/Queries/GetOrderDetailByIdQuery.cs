using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Orders.Queries;

public record GetOrderDetailByIdQuery(int OrderId, bool ForceRefresh) : ICachedQuery<List<OrderDetailViewModel>>
{
    public class Handler(IRepository<OrderDetail> repository, IMapper mapper) : IRequestHandler<GetOrderDetailByIdQuery, List<OrderDetailViewModel>>
    {
        public async Task<List<OrderDetailViewModel>> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository
                .Query(x => x.OrderId == request.OrderId)
                .Select(x => mapper.Map<OrderDetailViewModel>(x))
                .ToListAsync(cancellationToken);
            return result;
        }
    }

    public string Key => $"order-{OrderId}-details";

    public TimeSpan? Expiration => null;
}
