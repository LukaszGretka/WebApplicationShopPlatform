using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Catalog.Data;
using WebApplicationShopPlatform.Catalog.Models;
using WebApplicationShopPlatform.Catalog.Services.Abstract;
using WebApplicationShopPlatform.Shared.Models;
using WebApplicationShopPlatform.Shared.ModelsDTO;

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

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _productDbContext.Products.Where(product => product.Name.Equals(name)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(int categordyId)
        {
            return await _productDbContext.Products.Where(product => product.CategoryId == categordyId).ToListAsync();
        }

        public async Task<DatabaseActionResult<Product>> Create(ProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                CategoryId = productDTO.CategoryId,
                Amount = (int)(productDTO.Amount),
                NetPrice = (decimal)productDTO?.NetPrice
            };

            try
            {
                await _productDbContext.AddAsync(product);
                await _productDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new DatabaseActionResult<Product>(false, exception: ex);
            }

            return new DatabaseActionResult<Product>(true, obj: product);
        }

        public async Task<DatabaseActionResult<Product>> Update(int id, ProductDTO productDTO)
        {
            Product existingProduct = await _productDbContext.Products.FindAsync(id);

            if (existingProduct is null)
            {
                return new DatabaseActionResult<Product>(false, "Product no found");
            }

            existingProduct.Name = string.IsNullOrWhiteSpace(productDTO.Name) ? existingProduct.Name : productDTO.Name;
            existingProduct.Description = string.IsNullOrWhiteSpace(productDTO.Description) ? existingProduct.Description : productDTO.Description;
            existingProduct.CategoryId = string.IsNullOrEmpty(productDTO.CategoryId.ToString()) ? productDTO.CategoryId : existingProduct.CategoryId;
            existingProduct.Amount = productDTO.Amount ?? existingProduct.Amount;
            existingProduct.NetPrice = productDTO.NetPrice ?? existingProduct.NetPrice;

            try
            {
                await _productDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return new DatabaseActionResult<Product>(false, exception: ex);
            }

            return new DatabaseActionResult<Product>(true);
        }

        public async Task<DatabaseActionResult<Product>> DeleteById(int id)
        {
            Product foundProduct = await _productDbContext.Products.FindAsync(id);

            if (foundProduct is null)
            {
                return new DatabaseActionResult<Product>(false, "Product no found");
            }

            try
            {
                _productDbContext.Products.Remove(foundProduct);
                await _productDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return new DatabaseActionResult<Product>(false, exception: ex);
            }

            return new DatabaseActionResult<Product>(true);
        }
    }
}