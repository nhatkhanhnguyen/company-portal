using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;

namespace CompanyPortal.CQRS.Resources.Queries;

public record GetResourceByIdQuery(int ResourceId) : IRequest<ResourceViewModel>
{
    public class Handler(IRepository<Resource> repository, IMapper mapper)
        : IRequestHandler<GetResourceByIdQuery, ResourceViewModel>
    {
        public async Task<ResourceViewModel> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FirstOrDefaultAsync(request.ResourceId, cancellationToken);
            return mapper.Map<ResourceViewModel>(result);
        }
    }
}