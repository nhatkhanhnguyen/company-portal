using AutoMapper;

using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

namespace CompanyPortal;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductViewModel>()
            .ForMember(x => x.CategoryName, opt => opt.MapFrom(y => y.Category!.Name))
            .ForMember(x => x.CategoryId, opt => opt.MapFrom(y => y.Category!.Id))
            .ForMember(x => x.Images, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.Images, opt => opt.Ignore())
            .ForMember(x => x.Category, opt => opt.Ignore());
        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Article, ArticleViewModel>().ReverseMap();
        CreateMap<Order, OrderViewModel>().ReverseMap();
        CreateMap<Resource, ResourceViewModel>().ReverseMap();
        CreateMap<ContactRequest, ContactRequestViewModel>().ReverseMap();
    }
}
