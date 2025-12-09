using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.Models;
using TomadaStore.SaleConsumer.Repositories;
using TomadaStore.SaleConsumer.Repositories.Interfaces;
using TomadaStore.SaleConsumer.Services.Interfaces;

namespace TomadaStore.SaleConsumer.Services
{
    public class SaleConsumerService : ISaleConsumerService
    {
        private readonly ILogger<SaleConsumerService> _logger;
        private readonly ISaleConsumerRepository _saleConsumerRepository;

        public SaleConsumerService(ILogger<SaleConsumerService> logger, ISaleConsumerRepository saleConsumerRepository)
        {
            _logger = logger;
            _saleConsumerRepository = saleConsumerRepository;
        }

        public async Task ConsumeSaleAsync()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                    queue: "sales_approved_queue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    _logger.LogInformation($"Received message: {message}");

                      var sale = JsonSerializer.Deserialize<Sale>(message);

                      await _saleConsumerRepository.SaveSaleAsync(sale);

                      _logger.LogInformation($"Sale saved with Id: {sale.Id}");
                };

                await channel.BasicConsumeAsync(
                    queue: "sales_approved_queue",
                    autoAck: true,
                    consumer: consumer
                );

                _logger.LogInformation("Waiting for messages...");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error connecting to RabbitMQ: {ex.Message}");
            }
        }
    }
}
