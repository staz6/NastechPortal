namespace ProjectManagement.Dto
{
    public class ProjectTaskAsigneeGetDto
    {
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public int EmployeeId { get; set; }
        public int Id   {get;set;}
    }
}