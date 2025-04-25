using System.ComponentModel.DataAnnotations;

namespace Products.DataAccess.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int numberOfProducts { get; set; }
        public ICollection<Product> products { get; set; } = new HashSet<Product>();
    }
}
