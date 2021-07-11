using System.ComponentModel.DataAnnotations;

namespace UserManagement.Dto
{
    public class UpdateSalaryDto
    {
        [Required]
        public int Salary { get; set; }
    }
}