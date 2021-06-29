using System;
using System.Collections.Generic;

namespace SalaryManagment.Entities
{
    public class SalaryHistory 
    {
        
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<SalaryByMonth> SalaryByMonth {get;set;}
    }
}