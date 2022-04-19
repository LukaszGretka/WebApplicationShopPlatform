using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationShopPlatform.Catalog.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public int Amount { get; set; }

        public decimal NetPrice { get; set; }
    }
}
