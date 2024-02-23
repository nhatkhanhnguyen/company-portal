using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.ContactRequests.Queries;

public record GetAllQueryContactRequests : IRequest<IEnumerable<ContactRequestViewModel>>
{
    public class Handler(IRepository<ContactRequest> repository, IMapper mapper)
        : IRequestHandler<GetAllQueryContactRequests, IEnumerable<ContactRequestViewModel>>
    {
        public async Task<IEnumerable<ContactRequestViewModel>> Handle(GetAllQueryContactRequests request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAllListAsync(cancellationToken);
            return mapper.Map<List<ContactRequestViewModel>>(result);
        }
    }
}

