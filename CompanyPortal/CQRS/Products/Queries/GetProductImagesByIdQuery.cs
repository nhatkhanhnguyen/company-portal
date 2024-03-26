using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;

namespace CompanyPortal.CQRS.Products.Queries;

public record GetProductImagesByIdQuery(int ProductId, bool ForceRefresh = false) : ICachedQuery<List<ResourceViewModel>>
{
    public class Handler(IRepository<Resource> repository, IMapper mapper) : IRequestHandler<GetProductImagesByIdQuery, List<ResourceViewModel>>
    {
        public async Task<List<ResourceViewModel>> Handle(GetProductImagesByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllListAsync(x => x.ProductId == request.ProductId, cancellationToken);
            return mapper.Map<List<ResourceViewModel>>(result);
        }
    }

    public string Key => $"product-id-{ProductId}-images";

    public TimeSpan? Expiration => TimeSpan.FromDays(5);
}
