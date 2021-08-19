namespace ProjectManagement.Entites
{
    public class ProjectPhase : BaseClass
    {
        public ProjectSubFolder ProjectSubFolders { get; set; }
        public int ProjectSubFolderId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        
    }
}