using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Orders.BusinessLogic.RabbitMQ
{
    public class ProductNameUpdateConsumer : IProductNameUpdateConsumer, IDisposable
    {
        private readonly IConfiguration _configuration;
        private IChannel? _channel;
        private IConnection? _connection;
        private readonly ILogger<ProductNameUpdateConsumer> _logger;

        public ProductNameUpdateConsumer(IConfiguration configuration,
            ILogger<ProductNameUpdateConsumer> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var hostName = _configuration["RabbitMQ_Host"]!;
            var userName = _configuration["RabbitMQ_Username"]!;
            var password = _configuration["RabbitMQ_Password"]!;
            var port = _configuration["RabbitMQ_Port"]!;

            var connectionFactory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
                Port = int.Parse(port)
            };

            _logger.LogInformation("Connecting to RabbitMQ...");

            _connection = await connectionFactory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            _logger.LogInformation("Connected to RabbitMQ successfully.");
        }

        public void Consume()
        {
            if (_channel == null)
            {
                throw new InvalidOperationException("RabbitMQ channel is not initialized. Call InitializeAsync() first.");
            }

            var routingKey = "product.update.name";
            var queueName = "orders.product.update.name.queue";

            _channel.QueueDeclareAsync(queueName, true, false, false);

            var exchangeName = _configuration["RabbitMQ_Product_Exchange"]!;
            _channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, true);

            _channel.QueueBindAsync(queueName, exchangeName, routingKey);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var productNameUpdateMessage = JsonSerializer.Deserialize<ProductNameUpdateMessage>(message);

                if (productNameUpdateMessage is null)
                {
                    _logger.LogWarning("[RabbitMQ] Deserialized message is null.");
                    return;
                }

                _logger.LogInformation($"[RabbitMQ] Product name updated: {productNameUpdateMessage.ProductId} - {productNameUpdateMessage.NewName}");
                await _channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
            };

            _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }


}
