namespace ProjectManagement.Dto
{
    public class ProjectTaskGetDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string DueDate { get; set; }
        public int SubTask { get; set; }
    }
}