using Inventory_Management_System.Enums;
using Inventory_Management_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management_System.DTO
{
    public class AddTransactionDTO
    {
        public int Id { get; set; }
        [Required]
        public TransactionType Type { get; set; }
        [Required]
        public int QuantityChanged { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public int ProductId { get; set; }
        [Required]
        public int WarehouseId { get; set; }

        public int? FromWarehouseId { get; set; }
        public int? ToWarehouseId { get; set; }
    }
}
