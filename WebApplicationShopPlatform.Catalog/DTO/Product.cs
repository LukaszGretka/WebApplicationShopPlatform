using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopPlatform.Catalog.DTO
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        public int Amount { get; set; }

        public decimal NetPrice { get; set; }

        public decimal GrossPrice { get; set; }
    }
}
