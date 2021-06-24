using AttendanceManagment.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Attendance> Attendances { get; set; }
    }
}