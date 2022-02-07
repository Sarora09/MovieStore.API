using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieStore.API.Data;
using MovieStore.API.Models;
using MovieStore.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MovieStoreContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MovieStoreDB"))); // Using configuration interface to read the connection string from appsettings file

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MovieStoreContext>().AddDefaultTokenProviders(); // To use the ASP.NET Core Identity
            services.AddControllers().AddNewtonsoftJson(); // Added NewtonsoftJson to use JsonPatchDocument in this application
            services.AddTransient<IMovieRepository, MovieRepository>(); // Dependency injection for Movies
            services.AddTransient<ICustomerRepositary, CustomerRepository>(); // Dependency injection for Customers
            services.AddTransient<IAccessRepositary, AccessRepositary>(); // Dependency injection for Access
            services.AddAutoMapper(typeof(Startup)); // To use the AutoMapper in this application for mapping values between entity class and model class with same property names
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
