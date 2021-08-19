namespace ProjectManagement.Entites
{
    public class ProjectTaskAsignee : BaseClass
    {
        public ProjectTask  ProjectTasks { get; set; }
        public int ProjectTaskId { get; set; }
        public string AsigneeId { get; set; }
    }
}