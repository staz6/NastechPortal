using System;

namespace SalaryManagement.Entities
{
    public class Appraisal : BaseClass
    {        
        public int Percent { get; set; }
        public string UserId { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
       
    }
}