using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Catalog.Models;
using WebApplicationShopPlatform.Shared.Models;

namespace WebApplicationShopPlatform.Catalog.Services.Abstract
{
    public interface IProductService
    {
        Task<Product> GetProductById(int id);

        Task<IEnumerable<Product>> GetProductsByName(string name);

        Task<IEnumerable<Product>> GetProductsByCategory(int caterogryId);

        Task<DatabaseActionResult<Product>> Create(ProductDTO product);

        Task<DatabaseActionResult<Product>> Update(int id, ProductDTO product);

        Task<DatabaseActionResult<Product>> DeleteById(int id);
    }
}
