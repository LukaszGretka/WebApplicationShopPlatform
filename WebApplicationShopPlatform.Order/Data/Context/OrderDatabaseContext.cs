using Microsoft.EntityFrameworkCore;
using WebApplicationShopPlatform.Order.DTOs;

namespace WebApplicationShopPlatform.Order.Data.Context
{
    public class OrderDatabaseContext : DbContext
    {
        public OrderDatabaseContext(DbContextOptions<OrderDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderDTO>()
                   .Property(order => order.ID)
                   .ValueGeneratedOnAdd();
        }

        public DbSet<OrderDTO> Orders { get; set; }
    }
}
