using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Articles.Queries;

public record GetAllQueryArticles : IRequest<IEnumerable<ArticleViewModel>>
{

    public class GetAllQueryHandlerArticles(IRepository<Article> repository, IMapper mapper)
        : IRequestHandler<GetAllQueryArticles, IEnumerable<ArticleViewModel>>
    {
        public async Task<IEnumerable<ArticleViewModel>> Handle(GetAllQueryArticles request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAllListAsync(cancellationToken);
            return mapper.Map<List<ArticleViewModel>>(result);
        }
    }
}