using Products.DataAccess.Entities;

namespace Products.DataAccess.RepositoriesContracts
{
    public interface IProductRepository
    {
        Task<bool> CreateProductAsync(Product product);
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product?> GetProductByIdAsync(Guid? id);
        Task<IReadOnlyList<Product>> SearchProductAsync(string? searchPhrase);
        Task<bool> DeleteAsync(Guid id);
        Task<Product?> UpdateProductAsync(Product product);

    }
}
