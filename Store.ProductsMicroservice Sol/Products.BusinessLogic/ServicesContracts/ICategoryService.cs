using Products.BusinessLogic.Dtos;
using Products.DataAccess.Entities;

namespace Products.BusinessLogic.ServicesContracts
{
    public interface ICategoryService
    {
        Task<CategoryDto?> CreateCategory(AddCategoryDto? input);
        Task<CategoryDto?> GetCategoryByCode(string Code);
        Task<CategoryAndProductsDto?> GetCategoryAndProductsByCode(string Code);

        Task<IReadOnlyList<CategoryDto>> SearchCategory(string name);
        Task<IReadOnlyList<CategoryDto>> GetAll();
        Task<bool> DeleteCategory(string Code);
        Task<CategoryDto?> UpdateCategory(AddCategoryDto? category);
    }
}
