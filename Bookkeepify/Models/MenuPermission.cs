using System.ComponentModel.DataAnnotations;

namespace Bookkeepify.Models
{
    public class MenuPermission
    {
        public int Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public int MenuId { get; set; }

        public Menu Menu { get; set; }

    }
}
