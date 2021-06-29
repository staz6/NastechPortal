using System.Collections.Generic;

namespace SalaryManagment.Entities
{
    public class SalaryByMonth
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Month { get; set; }
        public int Deduction { get; set; }
        public int NetAmount {get;set;}          
        
        
    }
}