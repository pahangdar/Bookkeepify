using System.ComponentModel.DataAnnotations;

namespace Bookkeepify.Models
{
    public enum InvoiceType
    {
        Invoice,
        SalesReceipt
    }
    public class Invoice : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        [Required]
        public InvoiceType InvoiceType { get; set; }
        [Required]
        public string UserId { get; set; }
        public ICollection<InvoiceDetail>? InvoiceDetails { get; set; }

        // Property for the total invoice amount
        public decimal TotalAmount => (InvoiceDetails == null) ? 0 : InvoiceDetails.Sum(detail => detail.Total);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Number <= 0)
            {
                yield return new ValidationResult("Invalid Number. Please ensure Number is greater than 0.");
            }
            if (DueDate < Date)
            {
                yield return new ValidationResult("Invalid DueDate. Please ensure DueDate is greater than Date.");
            }
        }

    }
}
