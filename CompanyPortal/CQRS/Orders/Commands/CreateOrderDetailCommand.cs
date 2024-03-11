using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Commands;

public record CreateOrderDetailCommand(OrderDetailViewModel OrderDetail) : IRequest<Result>
{
    public class Handler(IRepository<OrderDetail> repository, IUnitOfWork uow,
        ILogger<Handler> logger, IMapper mapper) : IRequestHandler<CreateOrderDetailCommand, Result>
    {
        public async Task<Result> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = mapper.Map<OrderDetail>(request.OrderDetail);
                await repository.InsertAsync(entity, cancellationToken);
                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(entity.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang lưu chi tiết đơn hàng vào CSDL.");
            }
        }
    }
}