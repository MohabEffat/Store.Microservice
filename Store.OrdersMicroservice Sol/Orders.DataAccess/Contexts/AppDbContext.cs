using Microsoft.EntityFrameworkCore;
using Orders.DataAccess.Entities;

namespace Orders.DataAccess.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.ShippingAddress)
                .WithOwner();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }
}
