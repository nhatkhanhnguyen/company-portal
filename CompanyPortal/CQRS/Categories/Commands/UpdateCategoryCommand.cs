using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;

namespace CompanyPortal.CQRS.Categories.Commands;

public record UpdateCategoryCommand(CategoryViewModel Category) : IRequest<Result>
{
    public class Handler(IRepository<Category> repository, IUnitOfWork uow,
        IMapper mapper, ILogger<Handler> logger) : IRequestHandler<UpdateCategoryCommand, Result>
    {
        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await repository.GetAsync(request.Category.Id, cancellationToken);
            if (category == null)
            {
                logger.LogError("Category with {Id} not found.", request.Category.Id);
                return Result.Error($"Danh mục có ID = {request.Category.Id} không tồn tại khi đang tiến hành lưu vào CSDL.");
            }

            try
            {
                mapper.Map(request.Category, category);
                repository.Update(category);
                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(category.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error<string>("Có lỗi xảy ra khi đang lưu danh mục vào CSDL.");
            }
        }
    }
}