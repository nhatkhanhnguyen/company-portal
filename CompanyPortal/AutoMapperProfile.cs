using AutoMapper;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

namespace CompanyPortal;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductViewModel>().ReverseMap();
        CreateMap<Article, ArticleViewModel>().ReverseMap();
        CreateMap<Order, OrderViewModel>().ReverseMap();
        CreateMap<Resource, ResourceViewModel>().ReverseMap();
        CreateMap<ContactRequest, ContactRequestViewModel>().ReverseMap();
    }
}
