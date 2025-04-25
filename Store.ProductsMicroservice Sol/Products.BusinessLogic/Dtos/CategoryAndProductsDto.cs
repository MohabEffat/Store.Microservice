namespace Products.BusinessLogic.Dtos
{
    public class CategoryAndProductsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int numberOfProducts { get; set; }
        public ICollection<ProductDto> Products { get; set; } = new HashSet<ProductDto>();

    }
}
