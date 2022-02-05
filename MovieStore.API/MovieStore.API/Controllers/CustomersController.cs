using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieStore.API.Models;
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
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepositary.GetAllCustomersAsync();

            return Ok(customers);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            var customer = await _customerRepositary.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);

        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewCustomer([FromBody] CustomerModel customerModel)
        {
            int newCustomerId = await _customerRepositary.AddNewCustomerAsync(customerModel);

            customerModel.Id = newCustomerId;

            return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomerId }, customerModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerModel customerModel)
        {
            int newCustomerId = await _customerRepositary.UpdateCustomerAsync(id, customerModel);

            // Will work if a record for the provided id in the route doesn't exist in the database
            if (newCustomerId == 0)
            {
                return NotFound();
            }

            // If the provided id in the route exists in the database, return the updated record to the client
            customerModel.Id = newCustomerId;

            return Ok(customerModel);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomerPatch([FromRoute] int id, [FromBody] JsonPatchDocument customerModel)
        {
            int newCustomerId = await _customerRepositary.UpdateCustomerPatchAsync(id, customerModel);

            // Will work if a record for the provided id in the route doesn't exist in the database
            if (newCustomerId == 0)
            {
                return NotFound();
            }

            // If the provided id in the route exists in the database, return OK to the client
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            int newCustomerId = await _customerRepositary.DeleteCustomerAsync(id);

            // Will work if a record for the provided id in the route doesn't exist in the database
            if (newCustomerId == 0)
            {
                return NotFound();
            }

            // If the provided id in the route exists in the database, return OK to the client
            return Ok();
        }
    }
}
