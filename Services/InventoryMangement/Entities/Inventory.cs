using System.ComponentModel.DataAnnotations;
using InventoryMangement.Entities;

namespace InventoryMangment.Entities
{
    public class Inventory : BaseClass
    {
        [Required]
        public string Item { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Description { get; set; }
    }
}