using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepositary _customerRepositary;

        public CustomersController(ICustomerRepositary customerRepositary)
        {
            _customerRepositary = customerRepositary;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllMovies()
        {
            var customers = await _customerRepositary.GetAllCustomersAsync();

            return Ok(customers);

        }
    }
}
