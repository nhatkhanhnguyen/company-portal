using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Articles.Commands;

public record UpdateArticleCommand(ArticleViewModel Article) : IRequest<bool>
{
    public class Handler(IMapper mapper, IRepository<Article> repository, IUnitOfWork uow)
        : IRequestHandler<UpdateArticleCommand, bool>
    {
        public async Task<bool> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await repository.GetAsync(request.Article.Id, cancellationToken);
            if (article == null)
            {
                return false;
            }

            mapper.Map(request.Article, article);
            repository.Update(article);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}