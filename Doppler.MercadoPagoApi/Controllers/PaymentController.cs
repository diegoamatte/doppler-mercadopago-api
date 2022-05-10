using Doppler.MercadoPagoApi.DopplerSecurity;
using Doppler.MercadoPagoApi.Models;
using MercadoPago.Client.Customer;
using MercadoPago.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Doppler.MercadoPagoApi.Controllers
{
    [Authorize]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly CustomerClient _customerClient;

        public PaymentController(CustomerClient customerClient)
        {
            _customerClient = customerClient;
        }

        [Authorize(Policies.OWN_RESOURCE_OR_SUPERUSER)]
        [HttpPost("/accounts/{accountname}/customer")]
        public async Task<IActionResult> CreateCustomerAsync([FromRoute] string accountname, CustomerDto customer)
        {
            var customerRequest = new CustomerRequest
            {
                Email = accountname,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
            try
            {
                var savedCustomer = await _customerClient.CreateAsync(customerRequest);
                return Ok(new { CustomerId = savedCustomer.Id });
            }
            catch (MercadoPagoApiException exception)
            {
                return BadRequest(exception.ApiError);
            }
        }
    }
}
