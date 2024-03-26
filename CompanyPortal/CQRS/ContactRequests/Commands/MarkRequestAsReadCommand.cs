using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.ContactRequests.Commands;

public record MarkRequestAsReadCommand(int RequestId) : IRequest<Result>
{
    public class Handler(IRepository<ContactRequest> repository, IUnitOfWork uow, ILogger<Handler> logger)
        : IRequestHandler<MarkRequestAsReadCommand, Result>
    {
        public async Task<Result> Handle(MarkRequestAsReadCommand request, CancellationToken cancellationToken)
        {
            var contactRequest = await repository.GetAsync(request.RequestId, cancellationToken);
            if (contactRequest == null)
            {
                logger.LogError("Request with {Id} not found.", request.RequestId);
                return Result.Error($"Yêu cầu có ID = {request.RequestId} không tồn tại khi đang tiến hành lưu vào CSDL.");
            }

            try
            {
                contactRequest.IsRead = true;
                repository.Update(contactRequest);
                var result = await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang lưu thông tin yêu cầu vào CSDL.");
            }
        }
    }
}
