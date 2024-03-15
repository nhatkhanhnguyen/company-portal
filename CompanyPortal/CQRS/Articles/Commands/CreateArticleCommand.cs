using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Core.Constants;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Articles.Commands;

public record CreateArticleCommand(ArticleViewModel Article) : IRequest<Result>
{
    public class Handler(IRepository<Article> repository, IUnitOfWork uow,
        IMapper mapper, ILogger<Handler> logger) : IRequestHandler<CreateArticleCommand, Result>
    {
        public async Task<Result> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = mapper.Map<Article>(request.Article);
                await repository.InsertAsync(entity, cancellationToken);
                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(entity.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi lưu bài viết vào CSDL. Vui lòng thử lại sau!");
            }
        }
    }
}