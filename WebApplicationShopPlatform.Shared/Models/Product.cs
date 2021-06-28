using WebApplicationShopPlatform.Shared.Enums;

namespace WebApplicationShopPlatform.Shared.Models
{
    public class Product
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public decimal NetPrice { get; set; }

        public int Amount { get; set; }
    }
}
