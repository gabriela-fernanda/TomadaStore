using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Repositories.Interfaces;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

namespace TomadaStore.SaleAPI.Services.v1
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleService> _logger;
        private readonly HttpClient _httpClientProduct;
        private readonly HttpClient _httpClientCustomer;

        public SaleService(ISaleRepository saleRepository, ILogger<SaleService> logger, HttpClient httpClientProduct, HttpClient httpClientCustomer)
        {
            _saleRepository = saleRepository;
            _logger = logger;
            _httpClientProduct = httpClientProduct;
            _httpClientCustomer = httpClientCustomer;
        }

        public async Task CreateSaleAsync(int idCustomer, string idProduct, SaleRequestDTO saleDto)
        {
            try
            {
                var customer = await _httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(idCustomer.ToString());

                var product = await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(idProduct);

                await _saleRepository.CreateSaleAsync(customer, product, saleDto);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }

        public async Task CreateSaleWithListAsync(int idCustomer, List<string> productIds)
        {
            try
            {
                var customer = await _httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(
                $"https://localhost:5001/api/v1/Customer/{idCustomer}");


                var productDTOs = new List<ProductResponseDTO>();
                foreach (var productId in productIds)
                {
                    var product = await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(
                    $"https://localhost:6001/api/v1/Product/{productId}");
                    productDTOs.Add(product);
                }

                await _saleRepository.CreateSaleWithListAsync(customer, productDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }
    }
}
