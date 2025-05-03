using System.ComponentModel.DataAnnotations.Schema;
using Inventory_Management_System.Enums;

namespace Inventory_Management_System.Models
{
    public class InventoryTransaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public int QuantityChanged { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;


        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }


        //[ForeignKey("User")]
        //public string UserId { get; set; }
        //public ApplicationUser User { get; set; }

        [ForeignKey("FromWarehouse")]
        public int? FromWarehouseId { get; set; }
        public Warehouse? FromWarehouse { get; set; }

        [ForeignKey("ToWarehouse")]
        public int? ToWarehouseId { get; set; }
        public Warehouse? ToWarehouse { get; set; }
    }
}
