using System;

namespace AttendanceManagment.Dto
{
    public class GetUserLeaveDto
    {
        public DateTime From { get; set; }
       public DateTime Till { get; set; }
       public string Reason { get; set; }
       public bool Status { get; set; }
       public bool DeductSalary {get;set;}
    }
}