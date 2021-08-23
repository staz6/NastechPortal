using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Dto
{
    public class ProjectMemberCreateDto
    {
        [Required]
        public string AsigneeId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}