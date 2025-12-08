using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.SaleAPI.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task CreateSaleAsync(CustomerResponseDTO customer,
                            ProductResponseDTO product,
                            SaleRequestDTO sale);
        Task CreateSaleWithListAsync(CustomerResponseDTO customerDTO, List<ProductResponseDTO> productDTOs);
    }
}
