using System.Collections.Generic;

namespace ProjectManagement.Entites
{
    public class ProjectSubFolder : BaseClass
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProjectPhase> ProjectPhases { get; set; }
        
        
    }
}