using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Products.Queries;

public record GetProductImagesByIdQuery(int ProductId, bool ForceLive = false) : ICachedQuery<List<ResourceViewModel>>
{
    public class Handler(IRepository<Resource> repository, IMapper mapper) : IRequestHandler<GetProductImagesByIdQuery, List<ResourceViewModel>>
    {
        public async Task<List<ResourceViewModel>> Handle(GetProductImagesByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.Query(x => x.ProductId == request.ProductId)
                .ToListAsync(cancellationToken: cancellationToken);
            return mapper.Map<List<ResourceViewModel>>(result);
        }
    }

    public bool ForceLiveData => ForceLive;

    public string Key => $"product-id-{ProductId}-images";

    public TimeSpan? Expiration => null;
}
