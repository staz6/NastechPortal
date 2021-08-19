using System;
using System.Collections.Generic;

namespace ProjectManagement.Entites
{
    public class Project : BaseClass
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Image { get; set; }
        public string ProjectLead { get; set; }
        public string Attachment { get; set; }
        public DateTime DueDate { get; set; }
        public int Budget { get; set; }
        public bool? Status { get; set; } = false;
        public List<ProjectMember> ProjectMembers { get; set; }
    }

    
}