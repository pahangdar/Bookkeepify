using System.ComponentModel.DataAnnotations;

namespace Bookkeepify.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
