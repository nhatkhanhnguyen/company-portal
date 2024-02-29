using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.Categories.Commands;

public record DeleteCategoryCommand(int CategoryId, bool ForceDelete = false) : IRequest<bool>
{
    public class Handler(IRepository<Category> repository, IUnitOfWork uow) : IRequestHandler<DeleteCategoryCommand, bool>
    {
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.CategoryId, request.ForceDelete, cancellationToken);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}