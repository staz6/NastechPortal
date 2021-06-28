using System;

namespace AttendanceManagment.Dto
{
    public class UserLeaveDto
    {
        public DateTime From {get;set;}
        public DateTime Till {get;set;}
        public string Reason {get;set;}
    }
}