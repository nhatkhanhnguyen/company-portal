using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Products.Queries;

public record GetProductByIdQuery(int ProductId, bool ForceRefresh = false) : ICachedQuery<ProductViewModel>
{
    public class Handler(IRepository<Product> repository, IMapper mapper)
        : IRequestHandler<GetProductByIdQuery, ProductViewModel>
    {
        public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FirstOrDefaultAsync(request.ProductId, cancellationToken);
            return mapper.Map<ProductViewModel>(result);
        }
    }

    public string Key { get; } = $"product-id-{ProductId}";

    public TimeSpan? Expiration => null;
}