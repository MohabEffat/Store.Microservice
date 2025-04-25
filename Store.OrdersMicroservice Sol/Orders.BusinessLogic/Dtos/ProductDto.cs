namespace Orders.BusinessLogic.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityInStock { get; set; }
    }
}
