using System;

namespace SalaryManagement.Dto
{
    public class GetSalaryHistoryDto
    {
        public int Amount { get; set; }
        public string Month { get; set; }
        public int Deduction { get; set; }
        public int NetAmount {get;set;}
        public string Status { get; set; }
    }
}