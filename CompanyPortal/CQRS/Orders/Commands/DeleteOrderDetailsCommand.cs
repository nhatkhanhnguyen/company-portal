﻿using CompanyPortal.Core.Common;
using CompanyPortal.Core.Constants;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Commands;

public record DeleteOrderDetailsCommand(List<int> OrderDetailIds, bool ForceDelete) : IRequest<Result>
{
    public class Handler(IRepository<OrderDetail> repository, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<DeleteOrderDetailsCommand, Result>
    {
        public async Task<Result> Handle(DeleteOrderDetailsCommand request, CancellationToken cancellationToken)
        {
			try
			{
				repository.Delete(x => request.OrderDetailIds.Contains(x.Id), request.ForceDelete);
                var result = await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(result);
            }
			catch (Exception ex)
			{
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi xóa đơn hàng khỏi CSDL. Vui lòng thử lại sau!");
            }
        }
    }
}
