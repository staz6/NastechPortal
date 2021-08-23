using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Dto
{
    public class ProjectPhaseCreateDto
    {  
        [Required]
        public int ProjectSubFolderId { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
    }
}