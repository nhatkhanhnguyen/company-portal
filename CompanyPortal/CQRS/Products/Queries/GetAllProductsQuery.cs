using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Products.Queries;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductViewModel>>
{
    public class Handler(IRepository<Product> repository, IMapper mapper)
        : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductViewModel>>
    {
        public async Task<IEnumerable<ProductViewModel>> Handle(GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAllListAsync(cancellationToken);
            return mapper.Map<List<ProductViewModel>>(result);
        }
    }
}