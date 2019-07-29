using AutoMapper;
using Core.DTOs.Products;
using Core.Entities;
using Core.Interfaces.Result.Product;

namespace Infrastructure.Services.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, IGetProductsResult>()
                .ForMember(dst => dst.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dst => dst.Code, opts => opts.MapFrom(src => src.Code))
                .ForMember(dst => dst.DisplayName, opts => opts.MapFrom(src => src.DisplayName))
                .ForMember(dst => dst.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dst => dst.ProductCategory, opts => opts.MapFrom(src => src.ProductCategory))
                .ForAllOtherMembers(x => x.Ignore()); 

            CreateMap<AddProductRequest, Product>()
                .ForMember(dst => dst.Code, opts => opts.MapFrom(src => src.Code))
                .ForMember(dst => dst.DisplayName, opts => opts.MapFrom(src => src.DisplayName))
                .ForMember(dst => dst.Description, opts => opts.MapFrom(src => src.Description))
                .AfterMap((src, dest) => 
                {
                    if (src.ProductCategoryId.HasValue)
                        dest.ProductCategoryId = src.ProductCategoryId.Value;
                })
                .ForAllOtherMembers(x => x.Ignore());

        }
    }
}
