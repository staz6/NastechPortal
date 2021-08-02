using InventoryMangment.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryMangement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<EmployeeNetworkDetail> EmployeeNetworkDetails { get; set; }
        public DbSet<Inventory> Inventorys { get; set; }
        public DbSet<InventoryRequest> InventoryRequests { get; set; }
        public DbSet<InventoryRequestApproval> InventoryRequestApprovals { get; set; }

    }
}