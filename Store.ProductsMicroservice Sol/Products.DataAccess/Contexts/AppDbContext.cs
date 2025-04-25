using Microsoft.EntityFrameworkCore;
using Products.DataAccess.Entities;

namespace Products.DataAccess.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(x => x.CreatedAt)
                .HasComputedColumnSql("GETDATE()");
            
            modelBuilder.Entity<Product>()
               .HasIndex(x => x.Code)
               .IsUnique();

            modelBuilder.Entity<Category>()
               .HasIndex(x => x.Code)
               .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany(p => p.products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Category> Categories { get; set; } 
    }
}
