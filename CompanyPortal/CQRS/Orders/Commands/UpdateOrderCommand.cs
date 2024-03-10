using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Commands;

public record UpdateOrderCommand(OrderViewModel Order) : IRequest<Result>
{
    public class Handler( IRepository<Order> repository, IUnitOfWork uow,
        ILogger<Handler> logger, IMapper mapper) : IRequestHandler<UpdateOrderCommand, Result>
    {
        public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await repository.GetAsync(request.Order.Id, cancellationToken);
            if (order == null)
            {
                logger.LogError("Order with {Id} not found.", request.Order.Id);
                return Result.Error($"Đơn hàng có ID = {request.Order.Id} không tồn tại khi đang tiến hành lưu vào CSDL.");
            }

            try
            {
                mapper.Map(request.Order, order);
                repository.Update(order);
                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(order.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang lưu đơn hàng vào CSDL.");
            }
        }
    }
}