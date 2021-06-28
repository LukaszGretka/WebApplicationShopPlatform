using Microsoft.EntityFrameworkCore;
using WebApplicationShopPlatform.Catalog.DTO;

namespace WebApplicationShopPlatform.Catalog.Data
{
    public class ProductDatabaseContext : DbContext
    {
        public ProductDatabaseContext(DbContextOptions<ProductDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductDTO>()
                   .HasIndex(u => u.Name)
                   .IsUnique();
        }

        public DbSet<ProductDTO> Products { get; set; }
    }
}
