using System.ComponentModel.DataAnnotations;

namespace Bookkeepify.Models
{
    public class Menu
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string ?Lable { get; set; }
        [Required]
        public string? Route { get; set; }
        public string? IconClass { get; set; }
        public ICollection<MenuPermission>? Permissions { get; set; }
    }
}
