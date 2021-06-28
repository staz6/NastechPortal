using System;

namespace AttendanceManagment.Entities
{
    public class Leave
    {
       public int Id { get; set; }
       public DateTime From { get; set; }
       public DateTime Till { get; set; }
       public string Reason { get; set; }
       public bool Status { get; set; }
       public bool DeductSalary {get;set;}
       public string UserId { get; set; }
       
    }
}