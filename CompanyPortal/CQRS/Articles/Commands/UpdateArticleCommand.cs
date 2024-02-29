using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Articles.Commands;

public record UpdateArticleCommand(ArticleViewModel Article) : IRequest<int>
{
    public class Handler(IMapper mapper, IRepository<Article> repository, IRepository<Resource> resourceRepository, IUnitOfWork uow)
        : IRequestHandler<UpdateArticleCommand, int>
    {
        public async Task<int> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await repository.GetAsync(request.Article.Id, cancellationToken);
            if (article == null)
            {
                return 0;
            }

            mapper.Map(request.Article, article);

            if (article.IsActive)
            {
                resourceRepository.Undelete(x => x.ArticleId == request.Article.Id);
            }
            else
            {
                resourceRepository.Delete(x => x.ArticleId == request.Article.Id);
            }

            repository.Update(article);
            await uow.SaveChangesAsync(cancellationToken);
            return article.Id;
        }
    }
}