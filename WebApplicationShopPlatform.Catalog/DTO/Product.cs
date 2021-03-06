using System.ComponentModel.DataAnnotations;
using WebApplicationShopPlatform.Catalog.DTO.Enums;

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

        public Category? Category { get; set; }

        public int? Amount { get; set; }

        public decimal? NetPrice { get; set; }
    }
}
