using Microsoft.EntityFrameworkCore;
using Products.DataAccess.Contexts;
using Products.DataAccess.Entities;
using Products.DataAccess.RepositoriesContracts;

namespace Products.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateCategoryAsync(Category category)
        {
            category.Name = category.Name.ToLower();
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategory(string Code)
        {
            var cate = await _context.Categories.FindAsync(Code);
            if (cate == null) return false;
            _context.Categories.Remove(cate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category?> GetCategoryAndProductsByCodeAsync(string Code)
        {
            var existingCategory = await _context.Categories
                .Include(x => x.products)
                .FirstOrDefaultAsync(x => x.Code == Code);
            if (existingCategory == null) return null;
            return existingCategory;
        }

        public async Task<Category?> GetCategoryByCodeAsync(string Code)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Code == Code);
            if (existingCategory == null) return null;
            return existingCategory;
        }

        public async Task<IReadOnlyList<Category>> SearchCategoryAsync(string? searchPhrase)
        {
            if (string.IsNullOrWhiteSpace(searchPhrase))
                return await GetAllAsync();

            searchPhrase = searchPhrase.ToLower();

            return await _context.Categories
                .AsNoTracking()
                .Where(p =>
                    p.Name.ToLower().Contains(searchPhrase) ||
                    p.Description.ToLower().Contains(searchPhrase))
                .ToListAsync();
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Code == category.Code);

            if (existingCategory == null) return null;

            _context.Entry(existingCategory).CurrentValues.SetValues(category);

            await _context.SaveChangesAsync();

            return existingCategory!;
        }
    }
}
