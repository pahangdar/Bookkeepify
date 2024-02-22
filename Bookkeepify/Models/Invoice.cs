using System.ComponentModel.DataAnnotations;

namespace Bookkeepify.Models
{
    public enum InvoiceType
    {
        Invoice,
        SalesReceipt
    }
    public class Invoice
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Description { get; set; }
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

    }
}
