namespace Products.BusinessLogic.RabbitMQ
{
    public interface IRabbitMQPublisher
    {
        void Publish<T>(T message, string routeKey);
    }
}
