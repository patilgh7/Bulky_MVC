using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    internal interface IRepository<T> where T : class
    {
        // T - Category
        IEnumerable<T> GetAll();

        // When you want to pass predicate that means condition check in method like Find() method or FirstorDefault() method
        // For that syntax => Expression<Func<T,bool>> filter
        T Get(Expression<Func<T,bool>> filter);

        void Add(T entity);
        void Remove(T entity);
        
        void RemoveRange(IEnumerable<T> entities);

       
    }
}
