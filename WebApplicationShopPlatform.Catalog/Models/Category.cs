using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopPlatform.Catalog.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}
