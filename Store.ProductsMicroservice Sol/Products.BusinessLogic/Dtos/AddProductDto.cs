namespace Products.BusinessLogic.Dtos
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string CategoryCode { get; set; }
        public double UnitPrice { get; set; }
        public int QuantityInStock { get; set; }
        public string PictureUrl { get; set; }
    }
}
