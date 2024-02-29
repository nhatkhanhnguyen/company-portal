using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Categories.Commands;

public record UpdateCategoryCommand(CategoryViewModel Category) : IRequest<int>
{
    public class Handler(IMapper mapper, IRepository<Category> repository, IUnitOfWork uow)
        : IRequestHandler<UpdateCategoryCommand, int>
    {
        public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await repository.GetAsync(request.Category.Id, cancellationToken);
            if (category == null)
            {
                return 0;
            }

            mapper.Map(request.Category, category);
            repository.Update(category);

            await uow.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }
}