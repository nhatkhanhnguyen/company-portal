using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Products.Queries;

public record GetStarredProductsQuery(bool ForceRefresh = false) : ICachedQuery<Result>
{
    public class Handler(IRepository<ProductVariant> repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<GetStarredProductsQuery, Result>
    {
        public async Task<Result> Handle(GetStarredProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await repository
                    .GetAll()
                    .Include(x => x.Product)
                    .Include(x => x.Product.Category)
                    .Include(x => x.Product.Images)
                    .OrderBy(x => x.Product.Rate)
                    .Select(x => mapper.Map<ProductViewModel>(x))
                    .Take(4)
                    .ToListAsync(cancellationToken);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang đọc danh sách sản phẩm từ CSDL.");
            }
        }
    }

    public string Key => "starred-products";

    public TimeSpan? Expiration => null;
}
