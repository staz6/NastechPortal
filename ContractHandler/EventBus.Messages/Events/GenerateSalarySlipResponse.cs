namespace EventBus.Messages.Events
{
    public class GenerateSalarySlipResponse
    {
        public int EmployeeId { get; set; }
        public int CurrentSalary { get; set; }
        public string Designation { get; set; }
        public string FatherName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string JoiningDate { get; set; }

    }
}