using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record UpdateResourceCommand(ResourceViewModel Resource) : IRequest<bool>
{

    public class UpdateProductCommandHandler(IMapper mapper, IRepository<Resource> repository, IUnitOfWork uow)
        : IRequestHandler<UpdateResourceCommand, bool>
    {
        public async Task<bool> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await repository.GetAsync(request.Resource.Id, cancellationToken);
            if (resource == null)
            {
                return false;
            }

            mapper.Map(request.Resource, resource);
            repository.Update(resource);
            return await uow.SaveChangesAsync(cancellationToken);
        }
    }
}
