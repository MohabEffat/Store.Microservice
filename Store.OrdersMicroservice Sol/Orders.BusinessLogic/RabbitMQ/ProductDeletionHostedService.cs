using Microsoft.Extensions.Hosting;

namespace Orders.BusinessLogic.RabbitMQ
{
    public class ProductDeletionHostedService : IHostedService
    {
        private readonly IProductNameUpdateConsumer _consumer;
        public ProductDeletionHostedService(IProductNameUpdateConsumer consumer)
        {
            _consumer = consumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _consumer.InitializeAsync();
            _consumer.Consume();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Dispose();
            return Task.CompletedTask;
        }
    }
}
