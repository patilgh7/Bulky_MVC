using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Category
        IEnumerable<T> GetAll(string? includeProperties = null);

        // When you want to pass predicate that means condition check in method like Find() method or FirstorDefault() method
        // For that syntax => Expression<Func<T,bool>> filter
        T Get(Expression<Func<T,bool>> filter, string? includeProperties = null, bool tracked = false);

        void Add(T entity);

        void Remove(T entity);
        
        void RemoveRange(IEnumerable<T> entities);

       
    }
}
