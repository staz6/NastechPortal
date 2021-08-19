namespace ProjectManagement.Entites
{
    public class ProjectTaskCommentReply : BaseClass
    {
        public ProjectTaskComment ProjectTaskComments { get; set; }
        public int ProjectTaskCommentId { get; set; }
        public string From { get; set; }
        public string Description { get; set; }
        
    }
}