using ANDDigital.Michael.Nee.API.Extensions;
using ANDDigital.Michael.Nee.API.Models;
using ANDDigital.Michael.Nee.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;

namespace ANDDigital.Michael.Nee.API
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var assembly = typeof(Program).GetTypeInfo().Assembly;

            services.AddAutoMapper(assembly);
            services.Add(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            services.Add(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));
            services.AddSingleton<IPhoneNumberService, PhoneNumberService>();


            SeedData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.ConfigureExceptionHandler(logger);

            app.UseMvc();
        }

        #region private

        private void SeedData()
        {
            //A
            PhoneNumber.CreatePhoneNumber("07700900259");
            PhoneNumber.CreatePhoneNumber("07700900527");
            PhoneNumber.CreatePhoneNumber("07700900548");
            PhoneNumber.CreatePhoneNumber("07700900652");
            PhoneNumber.CreatePhoneNumber("07700900721");
            PhoneNumber.CreatePhoneNumber("07700900176");
            PhoneNumber.CreatePhoneNumber("07700900039");
            PhoneNumber.CreatePhoneNumber("07700900700");
            var phonenumber = PhoneNumber.CreatePhoneNumber("07700900043");

            var customer = Customer.CreateCustomer("Michael Smith", "MS1998");
            phonenumber.AddCustomerToPhoneNumber(customer);
        }

        #endregion
    }
}
