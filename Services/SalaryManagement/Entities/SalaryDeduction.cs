using System;

namespace SalaryManagement.Entities
{
    public class SalaryDeduction : BaseClass
    {
        public string UserId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public bool DeductSalary { get; set; }
        
    }
}