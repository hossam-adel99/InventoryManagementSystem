using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.DTO
{
    public class AddWarehouseDTO
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [MinLength(2, ErrorMessage = "Name must be greater than one charachters")]
        [Required(ErrorMessage = "You must enter a name")]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
