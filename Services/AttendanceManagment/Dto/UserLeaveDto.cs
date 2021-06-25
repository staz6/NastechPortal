using System;

namespace AttendanceManagment.Dto
{
    public class UserLeaveDto
    {
        public string UserId{get;set;}
        public DateTime From {get;set;}
        public DateTime Till {get;set;}
        public string Reason {get;set;}
    }
}