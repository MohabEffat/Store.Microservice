using Microsoft.EntityFrameworkCore;
using Products.DataAccess.Contexts;
using Products.DataAccess.Entities;
using Products.DataAccess.RepositoriesContracts;

namespace Products.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateProductAsync(Product product)
        {
            if (product == null) return false;
            await _context.Products.AddAsync(product);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            return await SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
            => await _context.Products.AsNoTracking().ToListAsync();

        public async Task<Product?> GetProductByIdAsync(Guid? id)
            => await _context.Products.FindAsync(id);

        public async Task<IReadOnlyList<Product>> SearchProductAsync(string? searchPhrase)
        {
            if (string.IsNullOrWhiteSpace(searchPhrase))
                return await GetAllAsync();

            searchPhrase = searchPhrase.ToLower();

            return await _context.Products
                .AsNoTracking()
                .Where(p =>
                    p.Name.ToLower().Contains(searchPhrase) ||
                    p.Description.ToLower().Contains(searchPhrase))
                .ToListAsync();
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null) return null;

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            var success = await SaveChangesAsync();

            return success ? existingProduct : null;
        }
        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
