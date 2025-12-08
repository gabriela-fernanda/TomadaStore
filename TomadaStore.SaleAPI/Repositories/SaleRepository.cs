using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Repositories.Interfaces;

namespace TomadaStore.SaleAPI.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ILogger<SaleRepository> _logger;
        private readonly IMongoCollection<Sale> _mongoCollection;
        private readonly ConnectionDB _connection;

        public SaleRepository(ILogger<SaleRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _mongoCollection = _connection.GetMongoCollection();
        }


        public async Task CreateSaleAsync(CustomerResponseDTO customerDTO,
                                            ProductResponseDTO productDTO,
                                            SaleRequestDTO sale)
        {
            try
            {
                var products = new List<Product>();

                var category = new Category
                (
                    productDTO.Category.Name,
                    productDTO.Category.Description
                );

                var product = new Product
                (
                    productDTO.Id,
                    productDTO.Name,
                    productDTO.Description,
                    productDTO.Price,
                    category
                );

                products.Add(product);

                var customer = new Customer
                (
                    customerDTO.Id,
                    customerDTO.FirstName,
                    customerDTO.LastName,
                    customerDTO.Email,
                    customerDTO.PhoneNumber
                );
                await _mongoCollection.InsertOneAsync(new Sale
                (
                    customer,
                    products,
                    productDTO.Price
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }

        public async Task CreateSaleWithListAsync(CustomerResponseDTO customerDTO, List<ProductResponseDTO> productDTOs)
        {
            try
            {
                var customer = new Customer(
                    customerDTO.Id,
                    customerDTO.FirstName,
                    customerDTO.LastName,
                    customerDTO.Email,
                    customerDTO.PhoneNumber
                );

                var products = productDTOs.Select(p => new Product(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    new Category(p.Category.Name, p.Category.Description)
                )).ToList();

                var totalPrice = products.Sum(p => p.Price);
                var sale = new Sale(customer, products, totalPrice);


                await _mongoCollection.InsertOneAsync(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }
    }
}
