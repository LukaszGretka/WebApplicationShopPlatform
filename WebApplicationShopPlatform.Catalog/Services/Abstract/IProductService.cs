using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Catalog.DTO;
using WebApplicationShopPlatform.Catalog.Models;

namespace WebApplicationShopPlatform.Catalog.Services.Abstract
{
    public interface IProductService
    {
        Task<Product> GetProductById(int id);

        Task<IEnumerable<Product>> GetProductByName(string name);

        Task<DatabaseActionResult<Product>> Create(Product product);

        Task<DatabaseActionResult<Product>> Update(int id, Product product);

        Task<DatabaseActionResult<Product>> DeleteById(int id);
    }
}
