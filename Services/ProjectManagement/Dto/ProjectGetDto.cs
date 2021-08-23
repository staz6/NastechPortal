using System.Collections.Generic;

namespace ProjectManagement.Dto
{
    public class ProjectGetDto
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Image { get; set; }
        public string ProjectLead { get; set; }
        public string ProjectLeadName { get; set; }
        public string ProjectLeadEmail { get; set; }
        public string ProjectLeadProfileImage { get; set; }
        public int ProjectLeadEmployeeId { get; set; }
        public List<ProjectMemeberGetDto> Asignee { get; set; }
        public string Attachment { get; set; } 
        public string CreatedAt { get; set; }
        public string DueDate { get; set; }
        public int Budget { get; set; }
        public bool? Status { get; set; }
    }
}