using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.ProductAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProductAsync(ProductRequestDTO productDto);
        Task <List<ProductResponseDTO>> GetAllProductsAsync();
        Task<ProductResponseDTO> GetProductByIdAsync(string id);
    }
}
