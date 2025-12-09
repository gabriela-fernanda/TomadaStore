using TomadaStore.Models.Models;

namespace TomadaStore.SaleConsumer.Repositories.Interfaces
{
    public interface ISaleConsumerRepository
    {
        Task SaveSaleAsync(Sale sale);
    }
}
