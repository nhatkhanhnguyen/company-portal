using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record DeleteResourcesCommand(List<int> ResourceIds, bool ForceDelete = false) : IRequest<bool>
{
    public class Handler(IRepository<Resource> repository, IUnitOfWork uow) : IRequestHandler<DeleteResourcesCommand, bool>
    {
        public async Task<bool> Handle(DeleteResourcesCommand request, CancellationToken cancellationToken)
        {
            repository.Delete(x => request.ResourceIds.Contains(x.Id), request.ForceDelete);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}
