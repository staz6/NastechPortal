using System;
using System.Collections.Generic;
using EventBus.Messages.Models;

namespace EventBus.Messages.Events
{
    public class UserGetAttendanceEventResponse : IntegrationBaseEvent
    {
        
        public IEnumerable<AttendanceEventDto> Attendance {get;set;}

    }
}