namespace Inventory_Management_System.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<Stocktaking?> Stocktakings { get; set; }
    }
}
