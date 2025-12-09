using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.PaymentAPI.Services.Interfaces;

namespace TomadaStore.PaymentAPI.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task ProcessPaymentAsync(Sale sale)
        {
            try
            {
                var totalPrice = sale.Products.Sum(p => p.Price);

                if(totalPrice <= 1000) { 

                    sale.Aproved = true;

                    var factory = new ConnectionFactory { HostName = "localhost" };
                    using var connection = await factory.CreateConnectionAsync();
                    using var channel = await connection.CreateChannelAsync();

                    await channel.QueueDeclareAsync(queue: "sales_approved_queue", durable: false, exclusive: false, autoDelete: false,
                        arguments: null);

                    var saleMessage = JsonSerializer.Serialize(sale);

                    var body = Encoding.UTF8.GetBytes(saleMessage);

                    await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "sales_approved_queue", body: body);

                    Console.WriteLine($"Sale approved and sent to sales_approved_queue: {sale.Id}");
                }
                else
                {
                    Console.WriteLine($"Sale rejected due to total > 1000: {sale.Id}");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
