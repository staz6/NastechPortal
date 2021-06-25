using Microsoft.AspNetCore.Identity;

namespace UserManagment.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PersonalEmail { get; set; }
        public string ContactNumber { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId {get;set;}
        
    }
}