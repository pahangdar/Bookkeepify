using System.ComponentModel.DataAnnotations;

namespace Bookkeepify.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public int? DebtAccountId { get; set; }
        public Account DebtAccount { get; set; }
        public int? CreditAccountId { get; set; }
        public Account CreditAccount { get; set; }
    }
}
