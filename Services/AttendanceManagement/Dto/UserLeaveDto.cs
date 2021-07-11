using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Dto
{
    public class UserLeaveDto
    {
        
        [Required]
        public DateTime From {get;set;}
        [Required]
        public DateTime Till {get;set;}
        [Required]
        public string Reason {get;set;}
        [Required]
        public string Type {get;set;}
    }
}