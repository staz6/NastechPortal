using System;
using Microsoft.EntityFrameworkCore;
using Polly;
using SalaryManagement.Entities;

namespace SalaryManagement.Data
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
        public DbSet<SalaryByMonth> SalaryByMonths {get;set;}
        public void MigrateDb()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.EnsureCreated());
        }
    }
}