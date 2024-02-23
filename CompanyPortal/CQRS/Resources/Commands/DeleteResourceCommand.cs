using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record DeleteResourceCommand(int ResourceId, bool ForceDelete = false) : IRequest<bool>
{
    public class Handler(IRepository<Resource> repository, IUnitOfWork uow)
        : IRequestHandler<DeleteResourceCommand, bool>
    {
        public async Task<bool> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.ResourceId, request.ForceDelete, cancellationToken);
            return await uow.SaveChangesAsync();
        }
    }
}