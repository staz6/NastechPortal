using System;

namespace EventBus.Messages.Models
{
    public class GetUserResponseDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime ShiftStart {get;set;}
        public DateTime ShiftEnd{get;set;}
        
    }
}