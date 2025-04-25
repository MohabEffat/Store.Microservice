namespace Orders.BusinessLogic.RabbitMQ
{
    public interface IProductDeletionConsumer
    {
        void Consume();
        void Dispose();
        Task InitializeAsync();
    }
}