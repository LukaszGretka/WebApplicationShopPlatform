using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Catalog.DTO;
using WebApplicationShopPlatform.Catalog.DTO.Enums;
using WebApplicationShopPlatform.Catalog.Models;

namespace WebApplicationShopPlatform.Catalog.Services.Abstract
{
    public interface IProductService
    {
        Task<Product> GetProductById(int id);

        Task<IEnumerable<Product>> GetProductsByName(string name);

        Task<IEnumerable<Product>> GetProductsByCategory(Category category);

        Task<DatabaseActionResult<Product>> Create(Product product);

        Task<DatabaseActionResult<Product>> Update(int id, Product product);

        Task<DatabaseActionResult<Product>> DeleteById(int id);
    }
}
