using AutoMapper;
using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Products.Queries;

public record GetProductCategoryByIdQuery(int ProductId, bool ForceRefresh = false) : ICachedQuery<CategoryViewModel>
{
    public class Handler(IRepository<Product> repository, IMapper mapper) : IRequestHandler<GetProductCategoryByIdQuery, CategoryViewModel>
    {
        public async Task<CategoryViewModel> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository
                .Query(x => x.Id == request.ProductId)
                .Select(x => x.Category)
                .FirstOrDefaultAsync(cancellationToken);
            return mapper.Map<CategoryViewModel>(result);
        }
    }

    public string Key => $"product-id-{ProductId}-category";

    public TimeSpan? Expiration => TimeSpan.FromDays(5);
}
