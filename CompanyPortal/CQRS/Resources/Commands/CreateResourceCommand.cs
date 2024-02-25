using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record CreateResourceCommand(ResourceViewModel Resource) : IRequest<bool>
{
    public class Handler(IMapper mapper, IRepository<Resource> repository, IUnitOfWork uow)
        : IRequestHandler<CreateResourceCommand, bool>
    {
        public async Task<bool> Handle(CreateResourceCommand request,
            CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Resource>(request.Resource);
            await repository.InsertAsync(entity, cancellationToken);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}