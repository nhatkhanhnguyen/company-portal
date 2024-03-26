using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;

namespace CompanyPortal.CQRS.Categories.Queries;

public record GetCategoryByIdQuery(int CategoryId) : IRequest<CategoryViewModel>
{

    public class Handler(IRepository<Category> repository, IMapper mapper)
        : IRequestHandler<GetCategoryByIdQuery, CategoryViewModel>
    {
        public async Task<CategoryViewModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FirstOrDefaultAsync(request.CategoryId, cancellationToken);
            return mapper.Map<CategoryViewModel>(result);
        }
    }
}