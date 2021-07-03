using System;

namespace AttendanceManagement.Dto
{
    public class GetUserLeaveDto
    {
        public DateTime From { get; set; }
       public DateTime Till { get; set; }
       public string Reason { get; set; }
       public string LeaveType { get; set; }
       public bool Status { get; set; }
       public bool DeductSalary {get;set;}
    }
}