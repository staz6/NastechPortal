using System;
using static ProjectManagement.Helper.ProjectPiority;

namespace ProjectManagement.Entites
{
    public class ProjectSubTask : BaseClass
    {
        public ProjectTask ProjectTasks { get; set; }
        public int ProjectTaskId { get; set; }
        public string Description { get; set; }
        public string Legend { get; set; }
        public ProjectTaskPiority Piority { get; set; }
        public DateTime DueDate { get; set; }
    }
}