using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Commands;

public record UpdateOrderDetailCommand(OrderDetailViewModel OrderDetail) : IRequest<Result>
{
    public class Handler(IRepository<OrderDetail> repository, IUnitOfWork uow,
        ILogger<Handler> logger, IMapper mapper) : IRequestHandler<UpdateOrderDetailCommand, Result>
    {
        public async Task<Result> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = await repository.GetAsync(request.OrderDetail.Id, cancellationToken);
            if (orderDetail == null)
            {
                logger.LogError("Order detail with {Id} not found.", request.OrderDetail.Id);
                return Result.Error($"Chi tiết đơn hàng có ID = {request.OrderDetail.Id} không tồn tại khi đang tiến hành lưu vào CSDL.");
            }

            try
            {
                mapper.Map(request.OrderDetail, orderDetail);
                repository.Update(orderDetail);
                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(orderDetail.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang lưu chi tiết đơn hàng vào CSDL.");
            }
        }
    }
}