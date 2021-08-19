using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entites;
using ProjectManagement.Interface;

namespace ProjectManagement.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseClass
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if(spec.Criteria!=null)
            {
                query=query.Where(spec.Criteria);
            }
            query=spec.Includes.Aggregate(query,(current,include) => current.Include(include));
            return query;
        }
        
    }
}