using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MovieStore.API.Data;
using MovieStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Repository
{
    public class CustomerRepository : ICustomerRepositary
    {
        private readonly MovieStoreContext _context;
        private readonly IMapper _mapper;

        // Constructor to use the dependency injection so to get the instance for DbContext and IMapper
        // Dependency injection allows to get the instance of DbContext and the IMapper in the application because we have registered them in the Startup class
        // All the methods in this repository can now use the instances inside the constructor below
        public CustomerRepository(MovieStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Retrieves all the customers from the database and send it to the conntroller class requesting them
        public async Task<List<CustomerModel>> GetAllCustomersAsync()
        {
            // Retrieving the list of all customers from the database
            var retrievedRecords = await _context.Customers.ToListAsync();
            // Using auto mapper to convert the list of Customers type to list of CustomerModel type
            return _mapper.Map<List<CustomerModel>>(retrievedRecords);
        }

        // Retrieves all the customers from the database and send it to the conntroller class requesting them
        public async Task<CustomerModel> GetCustomerByIdAsync(int customerID)
        {
            // Retrieving the list of all customers from the database
            var retrievedRecord = await _context.Customers.FindAsync(customerID);
            // Using auto mapper to convert the list of Customers type to list of CustomerModel type
            return _mapper.Map<CustomerModel>(retrievedRecord);
        }

        // Adds a new customer to the database and returns the new customer record id number to the controller
        public async Task<int> AddNewCustomerAsync(CustomerModel customerModel)
        {
            var customer = new Customers()
            {
                FirstName = customerModel.FirstName,
                LastName = customerModel.LastName,
                Email = customerModel.Email,
                CreditCard = customerModel.CreditCard,
                Age = customerModel.Age
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer.Id;
        }

        // Updates a customer record in the database if the id exists. Otherwise, it returns 0
        // Used the try-catch block to notify the requesting client in case a record with provided id doesn't exist in the database
        public async Task<int> UpdateCustomerAsync(int id, CustomerModel customerModel)
        {
            try
            {
                var customer = new Customers()
                {
                    Id = id,
                    FirstName = customerModel.FirstName,
                    LastName = customerModel.LastName,
                    Email = customerModel.Email,
                    CreditCard = customerModel.CreditCard,
                    Age = customerModel.Age
                };

                _context.Customers.Update(customer);
                // Hitting the database only one time for performance
                await _context.SaveChangesAsync();

                return customer.Id;
            }
            catch
            {
                return 0;
            }

        }

        // Updates a customer record in the database if the id exists. Otherwise, it returns 0
        // Used the try-catch block to notify the requesting client in case a record with provided id doesn't exist in the database
        public async Task<int> UpdateCustomerPatchAsync(int id, JsonPatchDocument customerModel)
        {
            try
            {
                var customerRecord = await _context.Customers.FindAsync(id);
                customerModel.ApplyTo(customerRecord);
                await _context.SaveChangesAsync();

                return id;
            }
            catch
            {
                return 0;
            }

        }

        // Deletes a customer record in the database if the id exists. Otherwise, it returns 0
        // Used the try-catch block to notify the requesting client in case a record with provided id doesn't exist in the database
        public async Task<int> DeleteCustomerAsync(int id)
        {
            try
            {
                var customerRecord = await _context.Customers.FindAsync(id);
                _context.Customers.Remove(customerRecord);
                await _context.SaveChangesAsync();
                return id;
            }
            catch
            {
                return 0;
            }

        }
    }
}