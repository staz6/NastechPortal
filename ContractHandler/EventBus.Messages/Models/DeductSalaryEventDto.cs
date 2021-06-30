using System;

namespace EventBus.Messages.Models
{
    public class DeductSalaryEventDto
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}