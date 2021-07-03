using System;

namespace InventoryMangment.Entities
{
    public class InventoryRequest
    {
        public string Category { get; set; }
        public string RequestedBy { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}