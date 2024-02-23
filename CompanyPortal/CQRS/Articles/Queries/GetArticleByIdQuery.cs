using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Articles.Queries;

public record GetArticleByIdQuery(int ArticleId) : IRequest<ArticleViewModel>
{

    public class Handler(IRepository<Article> repository, IMapper mapper)
        : IRequestHandler<GetArticleByIdQuery, ArticleViewModel>
    {
        public async Task<ArticleViewModel> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FirstOrDefaultAsync(request.ArticleId, cancellationToken);
            return mapper.Map<ArticleViewModel>(result);
        }
    }
}