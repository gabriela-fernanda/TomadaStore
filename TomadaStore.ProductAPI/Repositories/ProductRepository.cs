using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.ProductAPI.Data;
using TomadaStore.ProductAPI.Repositories.Interfaces;

namespace TomadaStore.ProductAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMongoCollection<Product> _mongoCollection;
        private readonly ConnectionDB _connection;

        public ProductRepository(ILogger<ProductRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _mongoCollection = _connection.GetMongoColection();
        }

        public async Task CreateProductAsync(ProductRequestDTO productDto)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(new Product
                (
                    productDto.Name,
                    productDto.Description,
                    productDto.Price,
                    new Category
                    (
                        productDto.Category.Name,
                        productDto.Category.Description
                    )

                 ));
            }catch (Exception ex)
            {
                _logger.LogError($"Error creating product: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(string id)
        {
            try
            {
                var product =  await _mongoCollection.Find(p => p.Id == new ObjectId(id).ToString()).FirstOrDefaultAsync();

                ProductResponseDTO newProduct = new()
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = new CategoryResponseDTO
                    {
                        Id = product.Category.Id.ToString(),
                        Name = product.Category.Name,
                        Description = product.Category.Description
                    }
                };

                return newProduct;
            }
            
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving product: {ex.Message}");
                throw new Exception(ex.StackTrace);
            }
        }

        public Task<List<ProductResponseDTO>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
