using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using MediatR;

namespace CompanyPortal.CQRS.ContactRequests.Commands;

public record MarkRequestAsRespondedCommand(int RequestId) : IRequest<Result>
{
    public class Handler(IRepository<ContactRequest> repository, IUnitOfWork uow, ILogger<Handler> logger)
        : IRequestHandler<MarkRequestAsRespondedCommand, Result>
    {
        public async Task<Result> Handle(MarkRequestAsRespondedCommand request, CancellationToken cancellationToken)
        {
            var contactRequest = await repository.GetAsync(request.RequestId, cancellationToken);
            if (contactRequest == null)
            {
                logger.LogError("Request with {Id} not found.", request.RequestId);
                return Result.Error($"Yêu cầu có ID = {request.RequestId} không tồn tại khi đang tiến hành lưu vào CSDL.");
            }

            try
            {
                contactRequest.IsResponsed = true;
                repository.Update(contactRequest);
                var result = await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang lưu thông tin vào CSDL.");
            }
        }
    }
}