namespace ProjectManagement.Entites
{
    public class ProjectTaskLog : BaseClass
    {
        public ProjectTask ProjectTasks { get; set; }
        public int ProjectTaskId {get;set;}
        public string Type { get; set; }
        public string Description { get; set; }
    }
}