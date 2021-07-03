using System;

namespace SalaryManagment.Dto
{
    public class PostSalaryDeduction
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }
        
        
    }
}