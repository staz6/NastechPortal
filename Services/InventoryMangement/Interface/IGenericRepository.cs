using System.Collections.Generic;
using InventoryMangement.Entities;

namespace InventoryMangement.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IReadOnlyList<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        
        
    }
    
}