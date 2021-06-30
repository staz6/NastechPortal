using System.Collections.Generic;

namespace SalaryManagment.Entities
{
    public class Salary
    {
        public int Id { get; set; }       
        public string UserId { get; set; }
        public int Amount { get; set; }
        public SalaryBreakdown SalaryBreakdown {get;set;}
        public SalaryHistory SalaryHistory {get;set;}
        
    }
}