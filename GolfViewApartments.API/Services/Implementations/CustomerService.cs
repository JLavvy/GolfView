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

        // ================================
        // CREATE
        // ================================
        public async Task<CustomerResponseDto> CreateCustomerAsync(CustomerRequestDto request)
        {
            var existingCustomer = await _customerRepository.GetByEmailAsync(request.Email);

            if (existingCustomer != null)
            {
                _logger.LogInformation(
                    "Customer with email {Email} already exists. Returning existing customer.",
                    request.Email);

                return MapToResponseDto(existingCustomer);
            }

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
                "Customer created successfully. ID={CustomerId}, Email={Email}",
                customer.Id,
                customer.Email);

            return MapToResponseDto(customer);
        }

        // ================================
        // GET BY ID
        // ================================
        public async Task<CustomerResponseDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Customer with ID {id} not found");

            return MapToResponseDto(customer);
        }

        // ================================
        // GET BY EMAIL
        // ================================
        public async Task<CustomerResponseDto?> GetCustomerByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);

            return customer != null
                ? MapToResponseDto(customer)
                : null;
        }

        // ================================
        // GET ALL
        // ================================
        public async Task<List<CustomerResponseDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers
                .Select(MapToResponseDto)
                .ToList();
        }

        // ================================
        // UPDATE
        // ================================
        public async Task<CustomerResponseDto> UpdateCustomerAsync(int id, CustomerRequestDto request)
        {
            var customer = await _customerRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Customer with ID {id} not found");

            // Check if email is being changed and already exists
            if (!string.Equals(customer.Email, request.Email, StringComparison.OrdinalIgnoreCase))
            {
                var existingCustomer = await _customerRepository.GetByEmailAsync(request.Email);

                if (existingCustomer != null && existingCustomer.Id != id)
                {
                    throw new InvalidOperationException(
                        $"Another customer with email {request.Email} already exists");
                }
            }

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;
            customer.Phone = request.Phone;

            await _customerRepository.UpdateAsync(customer);

            _logger.LogInformation(
                "Customer updated successfully. ID={CustomerId}",
                customer.Id);

            return MapToResponseDto(customer);
        }

        // ================================
        // DELETE
        // ================================
        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Customer with ID {id} not found");

            await _customerRepository.DeleteAsync(customer);

            _logger.LogInformation(
                "Customer deleted successfully. ID={CustomerId}",
                id);
        }

        // ================================
        // MAPPING
        // ================================
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