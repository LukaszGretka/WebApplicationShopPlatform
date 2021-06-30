using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplicationShopPlatform.Order.Data;
using WebApplicationShopPlatform.Order.Data.Context;
using WebApplicationShopPlatform.Order.Services;
using WebApplicationShopPlatform.Order.Services.Abstract;

namespace WebApplicationShopPlatform.Order
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrdersDbRepository, OrdersDbRepository>();

            services.AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<OrderDatabaseContext>
                    (opt => opt.UseInMemoryDatabase("InMemoryOrdersDatabase"));

            services.AddMediatR(typeof(Startup));
        }

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
