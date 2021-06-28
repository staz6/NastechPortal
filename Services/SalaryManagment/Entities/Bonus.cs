using System;

namespace SalaryManagment.Entities
{
    public class Bonus : BaseClass
    {
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        
    }
}