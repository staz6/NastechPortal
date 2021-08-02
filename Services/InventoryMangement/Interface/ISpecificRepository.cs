using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryMangment.Entities;

namespace InventoryMangement.Interface
{
    public interface ISpecificRepository
    {
        Task<IReadOnlyList<InventoryRequest>> getEmployeeInventoryRequest(string userId);
    }
}