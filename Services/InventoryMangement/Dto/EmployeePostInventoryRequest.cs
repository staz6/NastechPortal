namespace InventoryMangement.Dto
{
    public class EmployeePostInventoryRequest
    {
        public int InventoryId { get; set; }
        public string RequestedBy { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}