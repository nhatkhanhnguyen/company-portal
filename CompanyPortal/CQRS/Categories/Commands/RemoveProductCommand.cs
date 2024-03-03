using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Categories.Commands;

public record RemoveProductCommand(int ProductId) : IRequest<bool>
{
    public class Handler(IRepository<Product> repository, IUnitOfWork uow) : IRequestHandler<RemoveProductCommand, bool>
    {
        public async Task<bool> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await repository.Query(x => x.Id == request.ProductId).FirstOrDefaultAsync(cancellationToken);
            if (product != null) {
                product.CategoryId = null;
                repository.Update(product);
                return await uow.SaveChangesAsync(cancellationToken);
            }

            return false;
        }
    }
}
