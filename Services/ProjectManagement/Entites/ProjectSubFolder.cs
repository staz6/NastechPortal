namespace ProjectManagement.Entites
{
    public class ProjectSubFolder : BaseClass
    {
        public Project Projects { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}