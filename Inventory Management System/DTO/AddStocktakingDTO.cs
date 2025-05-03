using Inventory_Management_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management_System.DTO
{
    public class AddStocktakingDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Stock { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int WarehouseId { get; set; }
        public bool IsDeleted { get; set; }=false;
    }
}
