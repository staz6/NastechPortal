using System;

namespace UserManagement.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int CurrentSalary { get; set; }
        public string FatherName { get; set; }
        public int BioMetricId { get; set; }
        public string Designation { get; set; }
        public string Status { get; set; }
        public DateTime JoiningDate { get; set; }
        public string EmergencyNumber { get; set; }
        public string ProfileImage { get; set; }
        public string CNIC { get; set; }
        public string Role { get; set; }
        public string ShiftTiming { get; set; }
        public AppUser  AppUser {get;set;}
        public string AppUserId { get; set; }
    }
}