using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

namespace CompanyPortal.CQRS.Articles.Commands;

public record DeleteArticleCommand(int ArticleId, bool ForceDelete = false) : IRequest<bool>
{
    public class Handler(IRepository<Article> repository, IUnitOfWork uow) : IRequestHandler<DeleteArticleCommand, bool>
    {
        public async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.ArticleId, request.ForceDelete, cancellationToken);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}