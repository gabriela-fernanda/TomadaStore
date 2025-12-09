using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Services.v2.Interfaces;

namespace TomadaStore.SaleAPI.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class SaleProducerController : ControllerBase
    {
        private readonly ILogger<SaleProducerController> _logger;
        private readonly ISaleProducerService _saleProducerService;
        public SaleProducerController(ILogger<SaleProducerController> logger, ISaleProducerService saleProducerService)
        {
            _logger = logger;
            _saleProducerService = saleProducerService;
        }

        [HttpPost("{idCostumer}")]
        public async Task<IActionResult> CreateSaleProducerAsync(int idCostumer, [FromBody] SaleRequestDTO saleDto)
        {
            _logger.LogInformation("Producing new sale message");
            var sale = await _saleProducerService.CreateSaleProducerWithListAsync(idCostumer, saleDto.ProductIds);
            await _saleProducerService.CreateSaleProducerAsync(sale);
            return Ok("Sale message produced");
        }
    }
}
