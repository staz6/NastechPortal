using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryMangement.Interface;
using Microsoft.EntityFrameworkCore;

namespace InventoryMangement.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> table = null;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public IReadOnlyList<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}