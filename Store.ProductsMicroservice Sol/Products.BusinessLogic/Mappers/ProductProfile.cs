using AutoMapper;
using Products.BusinessLogic.Dtos;
using Products.DataAccess.Entities;

namespace Products.BusinessLogic.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<ProductImageUrlResolver>());
            CreateMap<ProductDto, Product>();
            CreateMap<AddProductDto, Product>();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<AddCategoryDto, Category>().ReverseMap();
            CreateMap<Category, CategoryAndProductsDto>();

        }
    }
}
