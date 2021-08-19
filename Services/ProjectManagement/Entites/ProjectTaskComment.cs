namespace ProjectManagement.Entites
{
    public class ProjectTaskComment : BaseClass
    {
        public ProjectTask ProjectTasks { get; set; }
        public int ProjectTaskId { get; set; }
        public string From { get; set; }
        public string Description { get; set; }
    }
}