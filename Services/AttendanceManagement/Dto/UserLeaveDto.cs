using System;

namespace AttendanceManagement.Dto
{
    public class UserLeaveDto
    {
        public DateTime From {get;set;}
        public DateTime Till {get;set;}
        public string Reason {get;set;}
        public string Type {get;set;}
    }
}