using System.ComponentModel.DataAnnotations;

namespace Bookkeepify.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
