namespace Inventory_Management_System.DTO
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int CategoryId { get; set; }
        public int LowStockThreshold { get; set; }

    }
}
