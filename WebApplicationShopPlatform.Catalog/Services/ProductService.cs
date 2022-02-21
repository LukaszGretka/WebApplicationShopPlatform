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
            Product product = await _productDbContext.Products.FindAsync(id);
            return new ProductDTO
            {
                Amount = product.Amount,
                Category = (Shared.Enums.Category)product.Category.Id,
                Description = product.Description,
                Id = product.ID,
                Name = product.Name,
                NetPrice = product.NetPrice
            };
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByName(string name)
        {
            List<Product> products = await _productDbContext.Products.Where(product => product.Name.Equals(name)).ToListAsync();

            return ProductDTOWrapper(products);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(Shared.Enums.Category category)
        {
            List<Product> products = await _productDbContext.Products.Where(product => product.Category.Id == (int)category).ToListAsync();

            return ProductDTOWrapper(products);
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
            Product existingProduct = await _productDbContext.Products.FindAsync(product.Id);

            if (existingProduct is null)
            {
                return new DatabaseActionResult<ProductDTO>(false, "Product no found");
            }

            existingProduct.Name = string.IsNullOrWhiteSpace(product.Name) ? existingProduct.Name : product.Name;
            existingProduct.Description = string.IsNullOrWhiteSpace(product.Description) ? existingProduct.Description : product.Description;
            existingProduct.Category.Id = product.Category.HasValue ? (int)product.Category.Value : existingProduct.Category.Id;
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
            Product foundProduct = await _productDbContext.Products.FindAsync(id);

            if (foundProduct is null)
            {
                return new DatabaseActionResult<ProductDTO>(false, "Product no found");
            }

            try
            {
                _productDbContext.Products.Remove(foundProduct);
                await _productDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return new DatabaseActionResult<ProductDTO>(false, exception: ex);
            }

            return new DatabaseActionResult<ProductDTO>(true);
        }

        private List<ProductDTO> ProductDTOWrapper(List<Product> products)
        {
            List<ProductDTO> productsDto = new List<ProductDTO>();

            products.ForEach(x => productsDto.Add(new ProductDTO
            {
                Amount = x.Amount,
                Category = (Shared.Enums.Category)x.Category.Id,
                Description = x.Description,
                Id = x.ID,
                Name = x.Name,
                NetPrice = x.NetPrice
            }));

            return productsDto;
        }
    }
}