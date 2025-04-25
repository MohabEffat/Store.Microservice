using AutoMapper;
using Microsoft.Extensions.Configuration;
using Products.DataAccess.Entities;

namespace Products.BusinessLogic.Dtos
{
    internal class ProductImageUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";

            return null!;
        }
    }
}
