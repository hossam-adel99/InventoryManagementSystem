using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management_System.DTO
{
    public class AddProductDTO
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MinLength(2, ErrorMessage = "Name must be greater than one charachters")]
        [Required(ErrorMessage = "You must enter a name")]
        public string Name { get; set; } = null!;
     
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int CategoryId { get; set; }

        [Range(0, int.MaxValue)]
        public int LowStockThreshold { get; set; }
    }
}
