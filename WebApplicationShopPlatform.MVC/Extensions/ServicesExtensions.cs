using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApplicationShopPlatform.MVC.Data;

namespace WebApplicationShopPlatform.MVC.Extensions
{
    public static class ServicesExtensions
    {
        internal static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                builder.UseNpgsql(config.GetConnectionString("Postgres.ConnectionString"),
                                  options => options.SetPostgresVersion(new Version(9, 6, 21)));
            });

            return services;
        }
    }
}
