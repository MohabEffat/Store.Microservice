namespace Products.BusinessLogic.RabbitMQ
{
    public class UpdateProductNameMessage
    {
        public Guid ProductId { get; set; }
        public string NewName { get; set; }
    }
}
