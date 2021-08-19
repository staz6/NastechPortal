using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Dto
{
    public class ProjectMemberCreateDto
    {
        [Required]
        public string AsigneeId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public string Role { get; set; }
    }
}