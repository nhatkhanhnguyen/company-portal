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
                .Select(x => new OrderViewModel
                {
                    Id = x.Id,
                    OrderDetails = x.OrderDetails.Select(detail => new OrderDetailViewModel
                    {
                        Id = detail.Id,
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        Price = detail.Price,
                        OrderId = detail.OrderId,
                        ProductName = detail.Product.Name,
                        ProductImageUrl = detail.Product.Images.First().Url,
                    }).ToList(),
                    Address = x.Address,
                    DateCreated = x.DateCreated,
                    Email = x.Email,
                    Fullname = x.Fullname,
                    IsActive = x.IsActive,
                    Status = x.Status,
                    Total = x.Total,
                    PhoneNumber = x.PhoneNumber
                }).ToListAsync(cancellationToken);
            return mapper.Map<List<OrderViewModel>>(result);
        }
    }

    public string Key => "orders";

    public TimeSpan? Expiration => null;
}