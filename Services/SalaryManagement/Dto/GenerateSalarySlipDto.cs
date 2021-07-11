namespace SalaryManagement.Dto
{
    public class GenerateSalarySlipDto
    {
        public int EmployeeId { get; set; }
        public int CurrentSalary { get; set; }
        public string Designation { get; set; }
        public string FatherName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string JoiningDate { get; set; }
        public int Amount { get; set; }
        public int Absent { get; set; }
        public int Late { get; set; }
    }
}