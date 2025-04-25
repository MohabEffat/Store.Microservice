using Products.BusinessLogic.Dtos;

namespace Products.BusinessLogic.ServicesContracts
{
    public interface IProductService
    {
        Task<ProductDto?> CreateProduct(AddProductDto? input);
        Task<ProductDto?> GetProductById(Guid? id);
        Task<IReadOnlyList<ProductDto>> GetAllProducts();
        Task<IReadOnlyList<ProductDto>> SearchProduct(string? searchPhrase);
        Task<bool> DeleteProduct(Guid id);
        Task<ProductDto?> UpdateProduct(ProductDto? input);


    }
}
