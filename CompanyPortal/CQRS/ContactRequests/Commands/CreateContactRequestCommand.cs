using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.ContactRequests.Commands;

public record CreateContactRequestCommand(ContactRequestViewModel ContactRequest) : IRequest<bool>
{
    public class Handler(IMapper mapper, IRepository<ContactRequest> repository, IUnitOfWork uow)
        : IRequestHandler<CreateContactRequestCommand, bool>
    {
        public async Task<bool> Handle(CreateContactRequestCommand request,
            CancellationToken cancellationToken)
        {
            var entity = mapper.Map<ContactRequest>(request.ContactRequest);
            await repository.InsertAsync(entity, cancellationToken);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}