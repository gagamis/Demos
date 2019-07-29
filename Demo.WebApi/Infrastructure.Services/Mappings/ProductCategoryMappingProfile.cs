using AutoMapper;
using Core.Entities;
using Core.Interfaces.Result.ProductCategories;

namespace Infrastructure.Services.Mappings
{
    public class ProductCategoryMappingProfile : Profile
    {

        public ProductCategoryMappingProfile()
        {

        CreateMap<ProductCategory, IProductCategoryResult>()
                .ForMember(dst => dst.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dst => dst.DisplayName, opts => opts.MapFrom(src => src.DisplayName))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
