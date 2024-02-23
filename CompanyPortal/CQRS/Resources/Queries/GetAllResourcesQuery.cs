using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Queries;

public record GetAllResourcesQuery : IRequest<IEnumerable<ResourceViewModel>>
{
    public class Handler(IRepository<Resource> repository, IMapper mapper)
        : IRequestHandler<GetAllResourcesQuery, IEnumerable<ResourceViewModel>>
    {
        public async Task<IEnumerable<ResourceViewModel>> Handle(GetAllResourcesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAllListAsync(cancellationToken);
            return mapper.Map<List<ResourceViewModel>>(result);
        }
    }
}
