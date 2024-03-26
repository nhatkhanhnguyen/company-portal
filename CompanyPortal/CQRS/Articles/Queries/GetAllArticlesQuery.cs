using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Articles.Queries;

public record GetAllArticlesQuery(bool ForceRefresh = false) : ICachedQuery<List<ArticleViewModel>>
{
    public class Handler(IRepository<Article> repository, IMapper mapper)
        : IRequestHandler<GetAllArticlesQuery, List<ArticleViewModel>>
    {
        public async Task<List<ArticleViewModel>> Handle(GetAllArticlesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAll()
                .Select(x => new ArticleViewModel
                {
                    Id = x.Id,
                    IsActive = x.IsActive,
                    DateCreated = x.DateCreated,
                    CoverImage = new ResourceViewModel
                    {
                        Id = x.CoverImageId,
                        Status = UploadFileStatus.Old,
                        BlobName = x.CoverImage.BlobName,
                        Url = x.CoverImage.Url,
                        ArticleId = x.Id
                    },
                    Content = x.Content,
                    Description = x.Description,
                    DateDeleted = x.DateDeleted,
                    DateModified = x.DateModified,
                    Tags = x.Tags,
                    Title = x.Title,
                    Type = x.Type,
                    Views = x.Views
                }).ToListAsync(cancellationToken);
            return result;
        }
    }

    public string Key => "articles";

    public TimeSpan? Expiration => null;
}