using System.Collections.Generic;
using WebApplicationShopPlatform.Catalog.Models;
using WebApplicationShopPlatform.Shared.Models;
using WebApplicationShopPlatform.Shared.ModelsDTO;

namespace WebApplicationShopPlatform.Catalog.Extenstions
{
    public static class ProductHelpers
    {
        internal static List<ProductDTO> WrapToDTO(this List<Product> products)
        {
            List<ProductDTO> productsDto = new List<ProductDTO>();

            products.ForEach(x => productsDto.Add(new ProductDTO
            {
                Amount = x.Amount,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Name = x.Name,
                NetPrice = x.NetPrice
            }));

            return productsDto;
        }
    }
}
