namespace AttendanceManagement.Dto
{
    public class GetLeaveHistoryDto
    {
        public int Total { get; set; }
        public int Remaining { get; set; }
        public int Taken {get;set;}
        public int Balance { get; set; }
    }
}