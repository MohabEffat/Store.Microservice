namespace Orders.BusinessLogic.RabbitMQ
{
    public class ProductDeletionMessage
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
    }
}
