using MovieStore.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MovieStoreContext _context;

        public CustomerRepository(MovieStoreContext context)
        {
            _context = context;
        }
    }
}
