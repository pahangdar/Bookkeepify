using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookkeepify.Models
{
    public class InvoiceDetail : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public decimal Total => Quantity * Price;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Quantity <= 0)
            {
                yield return new ValidationResult("Invalid Quantity. Please ensure Quantity is greater than 0.");
            }
            if (Price <= 0)
            {
                yield return new ValidationResult("Invalid Price. Please ensure Price is greater than 0.");
            }
        }

    }
}
