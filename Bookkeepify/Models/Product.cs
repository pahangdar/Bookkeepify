using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookkeepify.Models
{
    public class Product : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price <= 0 )
            {
                yield return new ValidationResult("Invalid Price amount. Please ensure Price is greater than 0.");
            }
        }

    }
}
