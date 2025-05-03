using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management_System.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; } = false;

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
