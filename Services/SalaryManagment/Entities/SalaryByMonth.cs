using System;
using System.Collections.Generic;

namespace SalaryManagment.Entities
{
    public class SalaryByMonth
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Month { get; set; }
        public int Deduction { get; set; }
        public int NetAmount {get;set;}
        public string UserId { get; set; }
        public SalaryHistory SalarHistory { get; set; }
        public int SalaryHistoryId { get; set; }
        public bool Status { get; set; }
        
        
    }
}