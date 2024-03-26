using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Core.Constants;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;

namespace CompanyPortal.CQRS.Articles.Commands;

public record UpdateArticleCommand(ArticleViewModel Article) : IRequest<Result>
{
    public class Handler(
        IRepository<Article> articleRepository, IRepository<Resource> resourceRepository, IUnitOfWork uow,
        IMapper mapper, ILogger<Handler> logger) : IRequestHandler<UpdateArticleCommand, Result>
    {
        public async Task<Result> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.GetAsync(request.Article.Id, cancellationToken);
            if (article == null)
            {
                logger.LogError("Article with {Id} not found.", request.Article.Id);
                return Result.Error($"Bài viết có ID = {request.Article.Id} không tồn tại khi đang tiến hành lưu vào CSDL.");
            }

            try
            {
                mapper.Map(request.Article, article);
                articleRepository.Update(article);
                if (article.IsActive)
                {
                    resourceRepository.Undelete(x => x.ArticleId == request.Article.Id);
                }
                else
                {
                    resourceRepository.Delete(x => x.ArticleId == request.Article.Id);
                }

                await uow.SaveChangesAsync(cancellationToken);
                return Result.Ok(article.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi cập nhật bài viết vào CSDL. Vui lòng thử lại sau!");
            }
        }
    }
}