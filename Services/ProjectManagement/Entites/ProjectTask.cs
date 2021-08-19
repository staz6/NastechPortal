using System;
using System.Collections.Generic;
using static ProjectManagement.Helper.ProjectPiority;

namespace ProjectManagement.Entites
{
    public class ProjectTask : BaseClass
    {
        public ProjectPhase ProjectPhases { get; set; }
        public int ProjectPhaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Attachement { get; set; }
        public ProjectTaskPiority Piority { get; set; }
        public string Reporter { get; set; }
        public List<ProjectTaskAsignee> ProjectAsignee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Position { get; set; }
        public List<ProjectTaskLog> ProjectTaskLogs{get;set;}
    }
    
}