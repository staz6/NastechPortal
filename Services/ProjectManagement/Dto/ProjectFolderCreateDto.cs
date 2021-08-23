using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Dto
{
    public class ProjectFolderCreateDto
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}