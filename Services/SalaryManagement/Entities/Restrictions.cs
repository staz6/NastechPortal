namespace SalaryManagement.Entities
{
    public class Restrictions : BaseClass
    {
        public int AllowedHoliday { get; set; }
        public int AllowedLates { get; set; }
        public int HolidayDeduction { get; set; }
    }
}