using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Dto
{
    public class AdminEditLeaveRequest
    {
        [Required]
        public bool Status { get; set; }
        [Required]
        public bool DeductSalary { get; set; }
    }
}