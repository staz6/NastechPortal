namespace ProjectManagement.Entites
{
    public class ProjectMember : BaseClass
    {
        public string AsigneeId { get; set; }
        public string Role { get; set; }
        public Project Projects { get; set; }
        public int ProjectId { get; set; }
    }


}