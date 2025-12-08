using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace TomadaStore.SaleAPI.Services.v2.Interfaces
{
    public interface ISaleProducerService
    {
        Task CreateSaleProducerAsync(Sale sale);
        Task<Sale> CreateSaleProducerWithListAsync(int idCustomer, List<string> productsDTOs);
    }
}
