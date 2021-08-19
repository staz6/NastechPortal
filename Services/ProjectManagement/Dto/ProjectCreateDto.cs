using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Dto
{
    public class ProjectCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Image { get; set; }
        [Required]
        public string ProjectLead { get; set; }
        public List<ProjectMemberCreateDto> ProjectMembers { get; set; }
        public string Attachment { get; set; } 
        [Required]  
        public DateTime DueDate { get; set; }
        public int Budget { get; set; }
    }
    
}