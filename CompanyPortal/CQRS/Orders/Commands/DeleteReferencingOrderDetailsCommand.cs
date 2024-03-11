using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using MediatR;

namespace CompanyPortal.CQRS.Orders.Commands;

public record DeleteReferencingOrderDetailsCommand(int OrderId, bool ForceDelete) : IRequest<Result>
{
    public class Handler(IRepository<OrderDetail> repository, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<DeleteReferencingOrderDetailsCommand, Result>
    {
        public async Task<Result> Handle(DeleteReferencingOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                repository.Delete(x => x.OrderId == request.OrderId, request.ForceDelete);
                var result = await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error<string>("Có lỗi xảy ra khi đang xóa chi tiết đặt hàng khỏi CSDL.");
            }
        }
    }
}
