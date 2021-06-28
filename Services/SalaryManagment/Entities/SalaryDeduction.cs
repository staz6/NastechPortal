using System;

namespace SalaryManagment.Entities
{
    public class SalaryDeduction : BaseClass
    {
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        
    }
}