using InventoryMangement.Entities;

namespace InventoryMangment.Entities
{
    public class Inventory : BaseClass
    {
       
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}