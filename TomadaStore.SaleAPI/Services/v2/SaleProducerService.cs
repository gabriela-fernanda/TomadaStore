using MongoDB.Driver;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Repositories.Interfaces;
using TomadaStore.SaleAPI.Services.v1;
using TomadaStore.SaleAPI.Services.v2.Interfaces;

namespace TomadaStore.SaleAPI.Services.v2
{
    public class SaleProducerService : ISaleProducerService
    {

        private readonly ILogger<SaleProducerService> _logger;
        private readonly HttpClient _httpClientProduct;
        private readonly HttpClient _httpClientCustomer;

        public SaleProducerService(ILogger<SaleProducerService> logger, HttpClient httpClientProduct, HttpClient httpClientCustomer)
        {
            _logger = logger;
            _httpClientProduct = httpClientProduct;
            _httpClientCustomer = httpClientCustomer;
        }

        public async Task CreateSaleProducerAsync(Sale sale)
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "sales_queue", durable: false, exclusive: false, autoDelete: false,
                    arguments: null);

                var saleMessage = JsonSerializer.Serialize<Sale>(sale);

                var body = Encoding.UTF8.GetBytes(saleMessage);

                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "sales_queue", body: body);

                _logger.LogInformation(" Sale message sent to the queue");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error producing sale message: {ex.Message}");
            }
        }

        public async Task<Sale> CreateSaleProducerWithListAsync(int idCustomer, List<string> productsDTOs)
        {
            try
            {
                var customerDTO = await _httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(
                $"https://localhost:5001/api/v1/Customer/{idCustomer}");

                var newCustomer = new Customer(
                    customerDTO.Id,
                    customerDTO.FirstName,
                    customerDTO.LastName,
                    customerDTO.Email,
                    customerDTO.PhoneNumber);

                var productDTOs = new List<Product>();
                foreach (var productId in productsDTOs)
                {
                    var product = await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(
                    $"https://localhost:6001/api/v1/Product/{productId}");
                    var newProduct = new Product(product.Id, product.Name, product.Description, product.Price, new Category(product.Category.Name, product.Category.Description));
                    productDTOs.Add(newProduct);
                }

                var totalPrice = productDTOs.Sum(p => p.Price);
                var sale = new Sale(newCustomer, productDTOs, totalPrice);

                return sale;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }
    }
}
