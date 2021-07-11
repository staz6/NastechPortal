using System;

namespace AttendanceManagement.Entities
{
    public class LeaveHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Total { get; set; }
        public int Remaining { get; set; }
        public int Year { get; set; }
    }
}