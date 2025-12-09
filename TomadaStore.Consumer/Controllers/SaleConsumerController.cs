using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.SaleConsumer.Services.Interfaces;

namespace TomadaStore.SaleConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleConsumerController : ControllerBase
    {
        private readonly ILogger<SaleConsumerController> _logger;
        private readonly ISaleConsumerService _saleConsumerService;

        public SaleConsumerController(ILogger<SaleConsumerController> logger, ISaleConsumerService saleConsumerService)
        {
            _logger = logger;
            _saleConsumerService = saleConsumerService;
        }

        [HttpPost("consume")]
        public async Task<IActionResult> ConsumeSaleAsync()
        {
            _logger.LogInformation("Starting to consume sales from RabbitMQ");

            await _saleConsumerService.ConsumeSaleAsync();

            return Ok("Sale consumption started");
        }
    }
}
