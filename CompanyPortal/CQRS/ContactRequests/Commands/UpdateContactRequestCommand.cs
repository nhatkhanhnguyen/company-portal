using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.ContactRequests.Commands;

public record UpdateContactRequestCommand(ContactRequestViewModel ContactRequest) : IRequest<bool>
{
    public class Handler(IMapper mapper, IRepository<ContactRequest> repository, IUnitOfWork uow)
        : IRequestHandler<UpdateContactRequestCommand, bool>
    {
        public async Task<bool> Handle(UpdateContactRequestCommand request, CancellationToken cancellationToken)
        {
            var contactRequest = await repository.GetAsync(request.ContactRequest.Id, cancellationToken);
            if (contactRequest == null)
            {
                return false;
            }

            mapper.Map(request.ContactRequest, contactRequest);
            repository.Update(contactRequest);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}
