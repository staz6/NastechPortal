using System;

namespace EventBus.Messages.Events
{
    public class UserGetAttendanceEventRequest 
    {
        public string UserId { get; set; }
        public DateTime Month { get; set; }
    }
}