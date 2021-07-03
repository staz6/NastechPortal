using System;

namespace SalaryManagment.Dto
{
    public class GetEmployeeSalaryDeduction
    {
        public string UserId { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }
        public string Reason { get; set; }
        public bool DeductSalary { get; set; }
    }
}