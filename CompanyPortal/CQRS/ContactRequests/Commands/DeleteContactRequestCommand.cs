using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.ContactRequests.Commands;

public record DeleteContactRequestCommand(int ContactRequestId, bool ForceDelete = false) : IRequest<Result>
{
    public class Handler(IRepository<ContactRequest> repository, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<DeleteContactRequestCommand, Result>
    {
        public async Task<Result> Handle(DeleteContactRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await repository.DeleteByIdAsync(request.ContactRequestId, request.ForceDelete, cancellationToken);
                var result = await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang xóa yêu cầu khỏi CSDL.");
            }
        }
    }
}