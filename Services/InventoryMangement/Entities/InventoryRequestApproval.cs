using System;

namespace InventoryMangment.Entities
{
    public class InventoryRequestApproval
    {
        public string ApprovedBy { get; set; }
        public DateTime Date { get; set; }
        public Inventory Inventory { get; set; }
        public int InventoryId { get; set; }
    }
}