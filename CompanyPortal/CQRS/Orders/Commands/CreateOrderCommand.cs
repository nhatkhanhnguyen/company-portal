using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Orders.Commands;

public record CreateOrderCommand(OrderViewModel Order) : IRequest<Result>
{
    public class Handler(IRepository<Order> repository, IUnitOfWork uow,
        ILogger<Handler> logger, IMapper mapper) : IRequestHandler<CreateOrderCommand, Result>
    {
        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        { 
            try
            {
                var entity = mapper.Map<Order>(request.Order);
                await repository.InsertAsync(entity, cancellationToken);
                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(entity.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error<string>("Có lỗi xảy ra khi đang lưu đơn hàng vào CSDL.");
            }
        }
    }
}