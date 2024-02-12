using System.ComponentModel.DataAnnotations;

namespace Bookkeepify.Models
{
    public class AccountType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}
