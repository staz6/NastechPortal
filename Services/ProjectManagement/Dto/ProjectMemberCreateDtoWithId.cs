using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Dto
{
    public class ProjectMemberCreateDtoWithId
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string AsigneeId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}