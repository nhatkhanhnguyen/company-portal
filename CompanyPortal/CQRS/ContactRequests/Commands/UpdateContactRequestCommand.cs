using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;

namespace CompanyPortal.CQRS.ContactRequests.Commands;

public record UpdateContactRequestCommand(ContactRequestViewModel ContactRequest) : IRequest<Result>
{
    public class Handler(IMapper mapper, IRepository<ContactRequest> repository, IUnitOfWork uow, ILogger<Handler> logger) : IRequestHandler<UpdateContactRequestCommand, Result>
    {
        public async Task<Result> Handle(UpdateContactRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contactRequest = await repository.GetAsync(request.ContactRequest.Id, cancellationToken);
                if (contactRequest == null)
                {
                    logger.LogError("Request with {Id} not found.", request.ContactRequest.Id);
                    return Result.Error($"Yêu cầu có ID = {request.ContactRequest.Id} không tồn tại khi đang tiến hành lưu vào CSDL.");
                }

                mapper.Map(request.ContactRequest, contactRequest);
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
