using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.ContactRequests.Commands;

public record DeleteContactRequestCommand(int ContactRequestId, bool ForceDelete = false) : IRequest<bool>
{
    public class Handler(IRepository<ContactRequest> repository, IUnitOfWork uow)
        : IRequestHandler<DeleteContactRequestCommand, bool>
    {
        public async Task<bool> Handle(DeleteContactRequestCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.ContactRequestId, request.ForceDelete, cancellationToken);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}