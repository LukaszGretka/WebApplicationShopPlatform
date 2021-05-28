using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Catalog.Data;
using WebApplicationShopPlatform.Catalog.DTO;
using WebApplicationShopPlatform.Catalog.Models;
using WebApplicationShopPlatform.Catalog.Services.Abstract;

namespace WebApplicationShopPlatform.Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDatabaseContext _productDbContext;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductDatabaseContext productDbContext, ILogger<ProductService> logger)
        {
            _productDbContext = productDbContext ?? throw new ArgumentNullException(nameof(productDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productDbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _productDbContext.Products.Where(product => product.Name.Equals(name)).ToListAsync();
        }

        public async Task<DatabaseActionResult<Product>> AddProduct(Product product)
        {
            try
            {
                await _productDbContext.AddAsync(product);
                await _productDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new DatabaseActionResult<Product>(false, ex.Message);
            }

            return new DatabaseActionResult<Product>(true, obj: product);
        }
    }
}
