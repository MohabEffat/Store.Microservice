namespace Products.BusinessLogic.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public double UnitPrice { get; set; }
        public int QuantityInStock { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; } = true;
        public string PictureUrl { get; set; }
    }
}
