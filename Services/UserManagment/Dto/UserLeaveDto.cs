using System;

namespace UserManagment.Dto.ServicesDto
{
    public class UserLeaveDto
    {
        public DateTime From {get;set;}
        public DateTime Till {get;set;}
        public  string  Reason {get;set;}
    }
}