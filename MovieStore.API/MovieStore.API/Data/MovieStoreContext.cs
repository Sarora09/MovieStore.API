using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Data
{
    public class MovieStoreContext : DbContext
    {
        public MovieStoreContext(DbContextOptions<MovieStoreContext> options) : base(options)
        {
        }

        public DbSet<Movies> Movies { get; set; }
    }
}
