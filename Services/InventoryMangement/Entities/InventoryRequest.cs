using System;
using InventoryMangement.Entities;

namespace InventoryMangment.Entities
{
    public class InventoryRequest : BaseClass
    {
        
        public string RequestedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateApproved{get;set;}
        public bool Status { get; set; }
        public bool Returned { get; set; }
        public Inventory Inventorys { get; set; }
        public int InventoryId { get; set; }
    }
}