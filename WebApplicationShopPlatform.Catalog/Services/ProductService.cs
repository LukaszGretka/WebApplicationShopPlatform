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
using WebApplicationShopPlatform.Shared.Enums;

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

        public async Task<ProductDTO> GetProductById(int id)
        {
            return await _productDbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByName(string name)
        {
            return await _productDbContext.Products.Where(product => product.Name.Equals(name)).ToListAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(Category category)
        {
            return await _productDbContext.Products.Where(product => product.Category == category).ToListAsync();
        }

        public async Task<DatabaseActionResult<ProductDTO>> Create(ProductDTO product)
        {
            try
            {
                await _productDbContext.AddAsync(product);
                await _productDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new DatabaseActionResult<ProductDTO>(false, exception: ex);
            }

            return new DatabaseActionResult<ProductDTO>(true, obj: product);
        }

        public async Task<DatabaseActionResult<ProductDTO>> Update(int id, ProductDTO product)
        {
            ProductDTO existingProduct = await _productDbContext.Products.FindAsync(product.ID);

            if (existingProduct is null)
            {
                return new DatabaseActionResult<ProductDTO>(false, "Product no found");
            }

            existingProduct.Name = string.IsNullOrWhiteSpace(product.Name) ? existingProduct.Name : product.Name;
            existingProduct.Description = string.IsNullOrWhiteSpace(product.Description) ? existingProduct.Description : product.Description;
            existingProduct.Category = product.Category ?? existingProduct.Category;
            existingProduct.Amount = product.Amount ?? existingProduct.Amount;
            existingProduct.NetPrice = product.NetPrice ?? existingProduct.NetPrice;

            try
            {
                await _productDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return new DatabaseActionResult<ProductDTO>(false, exception: ex);
            }

            return new DatabaseActionResult<ProductDTO>(true);
        }

        public async Task<DatabaseActionResult<ProductDTO>> DeleteById(int id)
        {
            ProductDTO foundProduct = await _productDbContext.Products.FindAsync(id);

            if (foundProduct is null)
            {
                return new DatabaseActionResult<ProductDTO>(false, "Product no found");
            }

            try
            {
                _productDbContext.Products.Remove(foundProduct);
                await _productDbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return new DatabaseActionResult<ProductDTO>(false, exception: ex);
            }

            return new DatabaseActionResult<ProductDTO>(true);
        }
    }
}
