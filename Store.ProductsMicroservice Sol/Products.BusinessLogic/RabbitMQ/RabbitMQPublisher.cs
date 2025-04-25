using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Products.BusinessLogic.RabbitMQ
{
    public class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IChannel _channel;
        private readonly IConnection _connection;


        public RabbitMQPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
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

            _connection = connectionFactory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult(); 
        }

        public void Publish<T>(T message, string routeKey)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var MessageBody = Encoding.UTF8.GetBytes(jsonMessage);
            var exchangeName = _configuration["RabbitMQ_Product_Exchange"]!;
            _channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, true);

            _channel.BasicPublishAsync(exchangeName, routeKey, MessageBody);

        }
        public void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
        }
    }
}
