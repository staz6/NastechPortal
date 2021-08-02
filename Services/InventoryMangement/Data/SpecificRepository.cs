using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryMangement.Interface;
using InventoryMangment.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryMangement.Data
{
    public class SpecificRepository : ISpecificRepository
    {
        private readonly AppDbContext _context;
        public SpecificRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<InventoryRequest>> getEmployeeInventoryRequest(string userId)
        {
            return await _context.InventoryRequests.Where(x=> x.RequestedBy == userId).ToListAsync();
        }
    }
}