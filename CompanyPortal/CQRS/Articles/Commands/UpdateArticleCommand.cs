using AutoMapper;

using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Articles.Commands;

public record UpdateArticleCommand(ArticleViewModel Article) : IRequest<bool>
{
    public class Handler(IMapper mapper, IRepository<Article> repository, IUnitOfWork uow)
        : IRequestHandler<UpdateArticleCommand, bool>
    {
        public async Task<bool> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Article>(request.Article);
            repository.Update(entity);
            return await uow.SaveChangesAsync();
        }
    }
}