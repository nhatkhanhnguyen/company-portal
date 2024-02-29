using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Categories.Commands;

public record CreateCategoryCommand(CategoryViewModel Article) : IRequest<int>
{
    public class Handler(IMapper mapper, IRepository<Category> repository, IUnitOfWork uow)
        : IRequestHandler<CreateCategoryCommand, int>
    {
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Category>(request.Article);
            await repository.InsertAsync(entity, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}