using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using MovieStore.API.Data;
using MovieStore.API.Models;
using MovieStore.API.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
                options => options.UseSqlServer(Configuration.GetConnectionString("MovieStoreContext"))); // Using configuration interface to read the connection string from appsettings file

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MovieStoreContext>().AddDefaultTokenProviders(); // To use the ASP.NET Core Identity

            // register authentication and perform authentication using JWT-bearer authentication
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            services.AddControllers().AddNewtonsoftJson(); // Added NewtonsoftJson to use JsonPatchDocument in this application
            services.AddTransient<IMovieRepository, MovieRepository>(); // Dependency injection for Movies
            services.AddTransient<ICustomerRepositary, CustomerRepository>(); // Dependency injection for Customers
            services.AddTransient<IAccessRepositary, AccessRepositary>(); // Dependency injection for Access
            services.AddAutoMapper(typeof(Startup)); // To use the AutoMapper in this application for mapping values between entity class and model class with same property names

            // To allow cross-origin resource sharing (CORS)
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddSwaggerGen(options =>
            {
                // using System.Reflection;
                // below code will configure the SwaggerGen to find the xml comments added in <summary> tag above action methods and show them in the UI
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API V1");
                c.DefaultModelsExpandDepth(-1); // To hide the schemas (models) in the swagger documentation
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication(); // enable authentication using JWT

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
