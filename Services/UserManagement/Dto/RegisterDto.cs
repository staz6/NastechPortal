using System;

namespace UserManagement.Dto
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string FatherName{get;set;}
        public string Address { get; set; }
        public string Email { get; set; }
        public string PersonalEmail {get;set;}
        public string ContactNumber { get; set; }
        public string Password { get; set; }
        public int BioMetricId { get; set; }
        public string ConfirmPassword {get;set;}
        public int CurrentSalary { get; set; }
        public string Designation { get; set; }
        public string Status { get; set; }
        public DateTime JoiningDate { get; set; }
        public string EmergencyNumber { get; set; }
        public string ProfileImage { get; set; }
        public string CNIC { get; set; }
        public string Role { get; set; } 
        public string ShiftTiming { get; set; }
    }
}