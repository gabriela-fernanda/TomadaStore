using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.CustomerAPI.Services;
using TomadaStore.CustomerAPI.Services.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                _logger.LogInformation("Creating a new customer");
                await _customerService.InsertCustomerAsync(customer);

                return Created();
            }catch (Exception e)
            {
                _logger.LogError(e, "Error occured while creating a new customer " + e.Message);
                return Problem(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResponseDTO>>> GetAllCustomerAsync()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return customers;
            }catch(Exception e)
            {
                _logger.LogError(e, "Error occured while getting the customers " + e.Message);
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDTO>> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occured while getting the customers " + e.Message);
                return Problem(e.Message);
            }
        }
    }
}
