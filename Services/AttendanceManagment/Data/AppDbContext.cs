using System;
using AttendanceManagment.Entities;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace AttendanceManagment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Leave> Leaves{get;set;}
        public void MigrateDb()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.EnsureCreated());
        }
    }
}