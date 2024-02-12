using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookkeepify.Models
{
    public class TransactionDetail : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        [Required]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DebitAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CreditAmount { get; set; }
        [Required]
        public int TransactionMethodId { get; set; }
        public TransactionMethod TransactionMethod { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((DebitAmount == 0 && CreditAmount == 0) || (DebitAmount > 0 && CreditAmount > 0))
            {
                yield return new ValidationResult("Invalid debit and credit amounts. Please ensure either debit or credit is greater than 0, and not both.");
            }
        }
    }
}
