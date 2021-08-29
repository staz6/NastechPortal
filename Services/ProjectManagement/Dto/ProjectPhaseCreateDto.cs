using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Dto
{
    public class ProjectPhaseCreateDto
    {  
        [Required]
        public int ProjectId { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
    }
}