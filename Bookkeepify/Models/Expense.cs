using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookkeepify.Models
{
    public class Expense : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Required]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public string UserId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Amount <= 0)
            {
                yield return new ValidationResult("Invalid Amount. Please ensure Amount is greater than 0.");
            }
        }

    }
}
