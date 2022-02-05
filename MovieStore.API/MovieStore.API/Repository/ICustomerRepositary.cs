using Microsoft.AspNetCore.JsonPatch;
using MovieStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Repository
{
    public interface ICustomerRepositary
    {
        Task<List<CustomerModel>> GetAllCustomersAsync();

        Task<CustomerModel> GetCustomerByIdAsync(int customerID);

        Task<int> AddNewCustomerAsync(CustomerModel customerModel);

        Task<int> UpdateCustomerAsync(int id, CustomerModel customerModel);

        Task<int> UpdateCustomerPatchAsync(int id, JsonPatchDocument customerModel);

        Task<int> DeleteCustomerAsync(int id);
    }
}
