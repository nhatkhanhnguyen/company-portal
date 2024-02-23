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
            var entity = mapper.Map<ContactRequest>(request.ContactRequest);
            repository.Update(entity);
            return await uow.SaveChangesAsync();
        }
    }
}
