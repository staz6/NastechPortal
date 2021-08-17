using System;

namespace InventoryMangement.Dto
{
    public class EmployeeInventoryRequest
    {
        public int Id { get; set; }

        public string RequestedBy { get; set; }
        // public string ApprovedBy { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public string DateApproved { get; set; }
        public string Status { get; set; }
        public bool Returned { get; set; }
        public string InventoryName { get; set; }
        public int InventoryId { get; set; }
    }
}