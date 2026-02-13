using GolfViewApartments.API.DTOs;
using GolfViewApartments.API.Models;
using GolfViewApartments.API.Repositories.Interfaces;
using GolfViewApartments.API.Services.Interfaces;

namespace GolfViewApartments.API.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(
            ICustomerRepository customerRepository,
            ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<CustomerResponseDto> CreateCustomerAsync(CustomerRequestDto request)
        {
            // Check if customer with this email already exists
            var existingCustomer = await _customerRepository.GetByEmailAsync(request.Email);
            
            if (existingCustomer != null)
            {
                _logger.LogInformation(
                    "Customer with email {Email} already exists, returning existing customer",
                    request.Email);
                
                return MapToResponseDto(existingCustomer);
            }

            // Create new customer
            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                CreatedAt = DateTime.UtcNow
            };

            await _customerRepository.AddAsync(customer);

            _logger.LogInformation(
                "Customer created: ID={CustomerId}, Email={Email}",
                customer.Id, customer.Email);

            return MapToResponseDto(customer);
        }

        public async Task<CustomerResponseDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Customer with ID {id} not found");

            return MapToResponseDto(customer);
        }

        public async Task<CustomerResponseDto?> GetCustomerByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            
            return customer != null ? MapToResponseDto(customer) : null;
        }

        public async Task<List<CustomerResponseDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers
                .Select(MapToResponseDto)
                .ToList();
        }

        private static CustomerResponseDto MapToResponseDto(Customer customer)
        {
            return new CustomerResponseDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}