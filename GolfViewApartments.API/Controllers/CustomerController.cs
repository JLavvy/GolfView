using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GolfViewApartments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            ICustomerService customerService,
            ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new customer or return existing one if email exists
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CustomerResponseDto>> CreateCustomer(
            [FromBody] CustomerRequestDto request)
        {
            try
            {
                var customer = await _customerService.CreateCustomerAsync(request);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                return StatusCode(500, new { message = "An error occurred while creating the customer" });
            }
        }

        /// <summary>
        /// Get customer by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                return Ok(customer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer {CustomerId}", id);
                return StatusCode(500, new { message = "An error occurred while retrieving the customer" });
            }
        }

        /// <summary>
        /// Get customer by email
        /// </summary>
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomerByEmail(string email)
        {
            try
            {
                var customer = await _customerService.GetCustomerByEmailAsync(email);
                
                if (customer == null)
                    return NotFound(new { message = $"Customer with email {email} not found" });

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer by email {Email}", email);
                return StatusCode(500, new { message = "An error occurred while retrieving the customer" });
            }
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<CustomerResponseDto>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all customers");
                return StatusCode(500, new { message = "An error occurred while retrieving customers" });
            }
        }
    }
}