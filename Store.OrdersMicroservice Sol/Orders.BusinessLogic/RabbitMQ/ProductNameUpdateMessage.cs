namespace Orders.BusinessLogic.RabbitMQ
{
    public class ProductNameUpdateMessage
    {
        public Guid ProductId { get; set; }
        public string NewName { get; set; }
    }
}
