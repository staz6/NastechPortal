using System;
using InventoryMangement.Entities;

namespace InventoryMangment.Entities
{
    public class InventoryRequestApproval : BaseClass
    {
        public string ApprovedBy { get; set; }
        public DateTime Date { get; set; }
        public Inventory Inventory { get; set; }
        public int InventoryId { get; set; }
    }
}