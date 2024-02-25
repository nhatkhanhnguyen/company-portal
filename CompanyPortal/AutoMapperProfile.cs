using AutoMapper;

using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

namespace CompanyPortal;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductViewModel>()
            .ForMember(x => x.Images, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.Images, opt => opt.Ignore());
        CreateMap<Article, ArticleViewModel>().ReverseMap();
        CreateMap<Order, OrderViewModel>().ReverseMap();
        CreateMap<Resource, ResourceViewModel>().ReverseMap();
        CreateMap<ContactRequest, ContactRequestViewModel>().ReverseMap();
    }
}
