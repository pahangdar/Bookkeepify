using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookkeepify.Models
{
    public class InvoiceDetail
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
    }
}
