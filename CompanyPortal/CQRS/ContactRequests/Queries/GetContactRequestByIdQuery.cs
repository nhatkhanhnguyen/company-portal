using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.ContactRequests.Queries;

public record GetContactRequestByIdQuery(int ContactRequestId) : IRequest<ContactRequestViewModel>
{
    public class Handler(IRepository<ContactRequest> repository, IMapper mapper)
        : IRequestHandler<GetContactRequestByIdQuery, ContactRequestViewModel>
    {
        public async Task<ContactRequestViewModel> Handle(GetContactRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FirstOrDefaultAsync(request.ContactRequestId, cancellationToken);
            return mapper.Map<ContactRequestViewModel>(result);
        }
    }

}
