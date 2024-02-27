using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record DeleteProductResourcesCommand(int ProductId, bool ForceDelete) : IRequest
{
    public class Handler(IRepository<Resource> repository, IUnitOfWork uow) : IRequestHandler<DeleteProductResourcesCommand>
    {
        public async Task Handle(DeleteProductResourcesCommand request, CancellationToken cancellationToken)
        {
            repository.Delete(x => x.ProductId == request.ProductId, request.ForceDelete);
            await uow.SaveChangesAsync(cancellationToken);
        }
    }
}
