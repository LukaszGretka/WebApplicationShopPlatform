using WebApplicationShopPlatform.Shared.ModelsDTO;

namespace WebApplicationShopPlatform.Shared.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public decimal? NetPrice { get; set; }

        public int? Amount { get; set; }
    }
}
