using AutoMapper;
using Products.BusinessLogic.Dtos;
using Products.BusinessLogic.ServicesContracts;
using Products.DataAccess.Entities;
using Products.DataAccess.Repositories;
using Products.DataAccess.RepositoriesContracts;

namespace Products.BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CategoryDto?> CreateCategory(AddCategoryDto? input)
        {

            var category = _mapper.Map<Category>(input);
            category.Id = Guid.NewGuid();

            var result = await _repository.CreateCategoryAsync(category);

            return result ? _mapper.Map<CategoryDto>(category) : null;
        }

        public async Task<bool> DeleteCategory(string Code)
        {
            var existingCategory = await _repository.GetCategoryByCodeAsync(Code);
            if (existingCategory == null)
                throw new ArgumentException($"Category '{Code}' not found.");
            return await _repository.DeleteCategory(Code);
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAll()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }


        public async Task<CategoryDto?> GetCategoryByCode(string Code)
        {
            var existingCategory = await _repository.GetCategoryByCodeAsync(Code);
            if (existingCategory == null)
                throw new ArgumentException($"Category '{Code}' not found.");
            return _mapper.Map<CategoryDto?>(existingCategory);
        }
        public async Task<CategoryAndProductsDto?> GetCategoryAndProductsByCode(string Code)
        {
            var existingCategory = await _repository.GetCategoryAndProductsByCodeAsync(Code);
            if (existingCategory == null)
                throw new ArgumentException($"Category '{Code}' not found.");
            return _mapper.Map<CategoryAndProductsDto?>(existingCategory);
        }

        public async Task<IReadOnlyList<CategoryDto>> SearchCategory(string searchPhrase)
        {
            var categories = await _repository.SearchCategoryAsync(searchPhrase);
            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> UpdateCategory(AddCategoryDto? category)
        {

            var updatedCategory = _mapper.Map<Category>(category);
            var result = await _repository.UpdateCategory(updatedCategory);
            if (result == null)
                throw new ArgumentException($"category '{category!.Code}' not found.");
            return _mapper.Map<CategoryDto?>(result);
        }
    }
}
