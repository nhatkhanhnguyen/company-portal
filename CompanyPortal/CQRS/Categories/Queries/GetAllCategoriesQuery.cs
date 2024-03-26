using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Categories.Queries;

public record GetAllCategoriesQuery(bool ForceRefresh = false) : ICachedQuery<List<CategoryViewModel>>
{
    public class Handler(IRepository<Category> repository, IMapper mapper)
        : IRequestHandler<GetAllCategoriesQuery, List<CategoryViewModel>>
    {
        public async Task<List<CategoryViewModel>> Handle(GetAllCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAll()
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    DateCreated = x.DateCreated,
                    ExternalId = x.ExternalId,
                    Image = mapper.Map<ResourceViewModel>(x.CoverImages.First()),
                    Products = mapper.Map<List<ProductViewModel>>(x.Products)
                }).ToListAsync(cancellationToken);
            return mapper.Map<List<CategoryViewModel>>(result);
        }
    }

    public string Key => "categories";

    public TimeSpan? Expiration => null;
}