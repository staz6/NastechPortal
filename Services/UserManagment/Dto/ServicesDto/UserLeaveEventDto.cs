using System;

namespace UserManagment.Dto.ServicesDto
{
    public class UserLeaveEventDto
    {
        public string UserId{get;set;}
        public DateTime From {get;set;}
        public DateTime Till {get;set;}
        public string Reason {get;set;}


    }
}