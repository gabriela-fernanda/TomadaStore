using MongoDB.Driver;
using TomadaStore.Models.Models;
using TomadaStore.SaleConsumer.Data;
using TomadaStore.SaleConsumer.Repositories.Interfaces;

namespace TomadaStore.SaleConsumer.Repositories
{
    public class SaleConsumerRepository : ISaleConsumerRepository
    {
        private readonly ILogger<SaleConsumerRepository> _logger;
        private readonly IMongoCollection<Sale> _mongoCollection;


        public SaleConsumerRepository(ILogger<SaleConsumerRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _mongoCollection = connection.GetMongoCollection();
        }

        public async Task SaveSaleAsync(Sale sale)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(sale);
                _logger.LogInformation("Sale saved to MongoDB successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving sale to MongoDB: {ex.Message}");
                throw;
            }
        }
    }
}
