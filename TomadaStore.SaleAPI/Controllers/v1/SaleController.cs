using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

namespace TomadaStore.SaleAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ILogger<SaleController> _logger;
        private readonly ISaleService _saleService;

        public SaleController(ILogger<SaleController> logger, ISaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }

        //[HttpPost("customer/{idCustomer}/product/{idProduct}")]
        //public async Task<IActionResult> CreateSaleAsync(int idCustomer, string idProduct, [FromBody] SaleRequestDTO saleDto)
        //{
        //    _logger.LogInformation("Creating new sale");

        //    await _saleService.CreateSaleAsync(idCustomer, idProduct, saleDto);
        //    return Ok("Sale created");
        //}

        [HttpPost("customer/{idCustomer}")]
        public async Task<IActionResult> CreateSaleAsync(int idCustomer, [FromBody] SaleRequestDTO saleDto)
        {
            _logger.LogInformation("Creating new sale");

            await _saleService.CreateSaleWithListAsync(idCustomer, saleDto.ProductIds);
            return Ok("Sale created");
        }
    }
}
