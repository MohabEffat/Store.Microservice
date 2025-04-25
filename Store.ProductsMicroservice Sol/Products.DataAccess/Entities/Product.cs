using System.ComponentModel.DataAnnotations;

namespace Products.DataAccess.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid CategoryId { get; set; }
        public double UnitPrice { get; set; }
        public int QuantityInStock { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; } = true;
        public string PictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
