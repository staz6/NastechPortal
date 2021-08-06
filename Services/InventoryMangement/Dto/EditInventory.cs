using System.ComponentModel.DataAnnotations;

namespace InventoryMangement.Dto
{
    public class EditInventory
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Item { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Description { get; set; }
    }
}