using System.ComponentModel.DataAnnotations;

namespace InventoryMangement.Dto
{
    public class EmployeePostInventoryRequest
    {
        [Required]
        public int InventoryId { get; set; }
        [Required]
        public string RequestedBy { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Subject { get; set; }
    }
}