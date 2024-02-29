using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Articles.Commands;

public record CreateArticleCommand(ArticleViewModel Article) : IRequest<int>
{
    public class Handler(IMapper mapper, IRepository<Article> repository, IUnitOfWork uow)
        : IRequestHandler<CreateArticleCommand, int>
    {
        public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Article>(request.Article);
            await repository.InsertAsync(entity, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}