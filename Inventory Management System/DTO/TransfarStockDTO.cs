using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.DTO
{
    public class TransfarStockDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Stock { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int FromWarehouseId { get; set; }
        [Required]
        public int ToWarehouseId { get; set; }
    }
}
