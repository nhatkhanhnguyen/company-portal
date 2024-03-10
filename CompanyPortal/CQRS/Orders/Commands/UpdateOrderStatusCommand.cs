using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enums;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Commands;

public record UpdateOrderStatusCommand(int OrderId, OrderStatus Status) : IRequest<Result>
{
    public class Handler(IRepository<Order> repository, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<UpdateOrderStatusCommand, Result>
    {
        public async Task<Result> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await repository.GetAsync(request.OrderId, cancellationToken);
                if (order == null)
                {
                    logger.LogError("Order with {Id} not found.", request.OrderId);
                    return Result.Error($"Đơn hàng có ID = {request.OrderId} không tồn tại khi đang tiến hành lưu vào CSDL.");
                }
                order.Status = request.Status;
                repository.Update(order);
                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(order.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error<string>("Có lỗi xảy ra khi đang lưu đơn hàng vào CSDL.");
            }
        }
    }
}
