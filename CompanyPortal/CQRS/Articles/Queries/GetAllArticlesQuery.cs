﻿using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Articles.Queries;

public record GetAllArticlesQuery(bool ForceRefresh = false) : ICachedQuery<List<ArticleViewModel>>
{
    public class Handler(IRepository<Article> repository, IMapper mapper)
        : IRequestHandler<GetAllArticlesQuery, List<ArticleViewModel>>
    {
        public async Task<List<ArticleViewModel>> Handle(GetAllArticlesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAllListAsync(cancellationToken);
            return mapper.Map<List<ArticleViewModel>>(result);
        }
    }

    public string Key => "articles";

    public TimeSpan? Expiration => null;
}