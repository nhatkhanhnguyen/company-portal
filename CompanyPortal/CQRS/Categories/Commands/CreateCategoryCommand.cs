using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Categories.Commands;

public record CreateCategoryCommand(CategoryViewModel Category) : IRequest<Result>
{
    public class Handler(IRepository<Category> repository, IUnitOfWork uow,
        IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<CreateCategoryCommand, Result>
    {
        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
             try 
            {
                var entity = mapper.Map<Category>(request.Category);
                await repository.InsertAsync(entity, cancellationToken);
                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(entity.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error<string>("Có lỗi xảy ra khi đang lưu danh mục vào CSDL.");
            }
        }
    }
}