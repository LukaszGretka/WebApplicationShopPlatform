using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Catalog.DTO;
using WebApplicationShopPlatform.Shared.Enums;
using WebApplicationShopPlatform.Shared.Models;

namespace WebApplicationShopPlatform.Catalog.Services.Abstract
{
    public interface IProductService
    {
        Task<ProductDTO> GetProductById(int id);

        Task<IEnumerable<ProductDTO>> GetProductsByName(string name);

        Task<IEnumerable<ProductDTO>> GetProductsByCategory(Category category);

        Task<DatabaseActionResult<ProductDTO>> Create(ProductDTO product);

        Task<DatabaseActionResult<ProductDTO>> Update(int id, ProductDTO product);

        Task<DatabaseActionResult<ProductDTO>> DeleteById(int id);
    }
}
