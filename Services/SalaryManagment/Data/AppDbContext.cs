using Microsoft.EntityFrameworkCore;
using SalaryManagment.Entities;

namespace SalaryManagment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Salary> Salarys { get; set; }
        public DbSet<Appraisal> Appraisals { get; set; }
        public DbSet<Bonus> Bonuss { get; set; }
        public DbSet<Restrictions> Restrictionss { get; set; }
        public DbSet<SalaryBreakdown> SalaryBreakdowns { get; set; }
        public DbSet<SalaryDeduction> SalaryDeductions { get; set; }
        public DbSet<SalaryHistory> SalaryHistorys { get; set; }
    }
}