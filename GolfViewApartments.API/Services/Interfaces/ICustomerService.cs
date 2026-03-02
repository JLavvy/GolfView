using GolfViewApartments.API.DTOs;

namespace GolfViewApartments.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> CreateCustomerAsync(CustomerRequestDto request);
        Task<CustomerResponseDto> GetCustomerByIdAsync(int id);
        Task<CustomerResponseDto?> GetCustomerByEmailAsync(string email);
        Task<List<CustomerResponseDto>> GetAllCustomersAsync();
        Task<CustomerResponseDto> UpdateCustomerAsync(int id, CustomerRequestDto request);
Task DeleteCustomerAsync(int id);
    }
}