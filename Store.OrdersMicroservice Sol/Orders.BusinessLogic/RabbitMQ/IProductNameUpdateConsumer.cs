namespace Orders.BusinessLogic.RabbitMQ
{
    public interface IProductNameUpdateConsumer
    {
        void Consume();
        void Dispose();
        Task InitializeAsync();
    }
}
