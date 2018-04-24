using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Angular_ASPNETCore_CustomersService.Repository;
using Swashbuckle.AspNetCore.Swagger;

namespace imwireless
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Add PostgreSQL support
            services.AddDbContext<CustomersDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("CustomersPostgresConnectionString"));
            });

            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IStatesRepository, StatesRepository>();
            services.AddTransient<CustomersDbSeeder>();

            //https://localhost:59373/swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ASP.NET Core Customers API",
                    Description = "ASP.NET Core/Angular Customers Swagger Documentation",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Shayane Umar"}
                });

            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CustomersDbSeeder customersDbSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();
            // Visit http://localhost:59373/swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });

            });

            customersDbSeeder.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}
