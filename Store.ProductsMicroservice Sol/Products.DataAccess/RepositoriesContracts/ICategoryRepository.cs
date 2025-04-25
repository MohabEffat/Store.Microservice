using Products.DataAccess.Entities;

namespace Products.DataAccess.RepositoriesContracts
{
    public interface ICategoryRepository
    {
        Task<bool> CreateCategoryAsync(Category category);
        Task<Category?> GetCategoryByCodeAsync(string Code);
        Task<IReadOnlyList<Category>> SearchCategoryAsync(string? searchPhrase);
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task<bool> DeleteCategory(string Code);
        Task<Category?> UpdateCategory(Category category);
        Task<Category?> GetCategoryAndProductsByCodeAsync(string Code);
    }
}
