using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Data
{
    public class MovieStoreContext : IdentityDbContext<ApplicationUser>
    {
        public MovieStoreContext(DbContextOptions<MovieStoreContext> options) : base(options)
        {
        }

        public DbSet<Movies> Movies { get; set; }

        public DbSet<Customers> Customers { get; set; }
    }
}
