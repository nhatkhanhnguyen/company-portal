using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductViewModel>
{
    public class Handler(IRepository<Product> repository, IMapper mapper)
        : IRequestHandler<GetProductByIdQuery, ProductViewModel>
    {
        public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FirstOrDefaultAsync(request.Id, cancellationToken);
            return mapper.Map<ProductViewModel>(result);
        }
    }
}