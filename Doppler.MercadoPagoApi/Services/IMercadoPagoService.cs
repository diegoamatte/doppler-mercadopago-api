using Doppler.MercadoPagoApi.Models;
using MercadoPago.Resource.Customer;
using System.Threading.Tasks;

namespace Doppler.MercadoPagoApi.Services
{
    public interface IMercadoPagoService
    {
        Task<Customer> CreateCustomerAsync(CustomerDto customer);
        Task<Customer> GetCustomerByEmailAsync(string email);
    }
}