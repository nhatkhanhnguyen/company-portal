using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Articles.Queries;

public record GetArticleImagesByIdQuery(int ArticleId, bool ForceRefresh = false) : ICachedQuery<List<ResourceViewModel>>
{
    public class Handler(IRepository<Resource> repository, IMapper mapper) : IRequestHandler<GetArticleImagesByIdQuery, List<ResourceViewModel>>
    {
        public async Task<List<ResourceViewModel>> Handle(GetArticleImagesByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllListAsync(x => x.ArticleId == request.ArticleId, cancellationToken);
            return mapper.Map<List<ResourceViewModel>>(result);
        }
    }

    public string Key => $"article-id-{ArticleId}-images";

    public TimeSpan? Expiration => TimeSpan.FromDays(5);
}
