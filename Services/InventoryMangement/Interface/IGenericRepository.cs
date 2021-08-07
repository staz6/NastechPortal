using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryMangement.Entities;

namespace InventoryMangement.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        Task Save();
        
        
    }
    
}