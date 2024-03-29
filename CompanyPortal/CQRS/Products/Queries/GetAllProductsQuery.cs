﻿using AutoMapper;

using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Products.Queries;

public record GetAllProductsQuery(bool ForceRefresh = false) : ICachedQuery<List<ProductViewModel>>
{
    public class Handler(IRepository<Product> repository, IMapper mapper)
        : IRequestHandler<GetAllProductsQuery, List<ProductViewModel>>
    {
        public async Task<List<ProductViewModel>> Handle(GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var result = await repository
                .GetAll()
                .Include(x => x.Category)
                .ToListAsync(cancellationToken);
            return mapper.Map<List<ProductViewModel>>(result);
        }
    }

    public string Key => "products";

    public TimeSpan? Expiration => null;
}