using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;

namespace CompanyPortal.CQRS.ContactRequests.Queries;

public record GetAllContactRequestsQuery(bool ForceRefresh) : ICachedQuery<List<ContactRequestViewModel>>
{
    public class Handler(IRepository<ContactRequest> repository, IMapper mapper)
        : IRequestHandler<GetAllContactRequestsQuery, List<ContactRequestViewModel>>
    {
        public async Task<List<ContactRequestViewModel>> Handle(GetAllContactRequestsQuery request,
            CancellationToken cancellationToken)
        {
            var result = await repository.GetAllListAsync(cancellationToken);
            return mapper.Map<List<ContactRequestViewModel>>(result);
        }
    }

    public string Key => "contact-requests";

    public TimeSpan? Expiration => null;
}

