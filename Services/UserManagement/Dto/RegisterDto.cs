using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Dto
{
    public class RegisterDto
    {
        [Required] 
        public string Name { get; set; }
        [Required]
        public string FatherName{get;set;}
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PersonalEmail {get;set;}
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        [MaxLength(8,ErrorMessage ="Password must be 8 character long")]
        public string Password { get; set; }
        [Required]
        [MaxLength(8,ErrorMessage ="Password must be 8 character long")]
        [Compare(nameof(Password),ErrorMessage = "Password do not match")]
        public string ConfirmPassword {get;set;}
        [Required]
        public int BioMetricId { get; set; }
        [Required]
        public int CurrentSalary { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
        [Required]
        public string EmergencyNumber { get; set; }
        public string ProfileImage { get; set; }
        [Required]
        public string CNIC { get; set; }
        [Required]
        public string Role { get; set; }
        [Required] 
        public string ShiftTiming { get; set; }
    }
}