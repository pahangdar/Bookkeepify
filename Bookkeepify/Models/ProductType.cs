using System.ComponentModel.DataAnnotations;

namespace Bookkeepify.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool HasInventory { get; set; }
        public ICollection<Product> Products { get; set;}
    }
}
