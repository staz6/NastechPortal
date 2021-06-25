using System;

namespace EventBus.Messages.Events
{
    public class UserLeaveEvent : IntegrationBaseEvent
    {
        public string UserId{get;set;}
        public DateTime From {get;set;}
        public DateTime Till {get;set;}
        public string Reason {get;set;}
        
    }
}