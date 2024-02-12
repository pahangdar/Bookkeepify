using System.ComponentModel.DataAnnotations;

namespace Bookkeepify.Models
{
    public class TransactionMethod
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
    }
}
