using System;
using System.Collections.Generic;

namespace SalaryManagement.Entities
{
    public class SalaryHistory 
    {
        
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<SalaryByMonth> SalaryByMonth {get;set;}
        
    }
}