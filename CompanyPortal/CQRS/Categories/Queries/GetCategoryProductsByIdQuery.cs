using AutoMapper;
using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Categories.Queries;

public record GetCategoryProductsByIdQuery(int CategoryId, bool ForceRefresh) : ICachedQuery<List<ProductViewModel>>
{
    public class Handler(IRepository<Product> repository, IMapper mapper) : IRequestHandler<GetCategoryProductsByIdQuery, List<ProductViewModel>>
    {
        public async Task<List<ProductViewModel>> Handle(GetCategoryProductsByIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.Query(x => x.CategoryId == request.CategoryId)
                .Select(x => mapper.Map<ProductViewModel>(x))
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }

    public string Key => $"category-{CategoryId}-products";
    public TimeSpan? Expiration => null;
}
