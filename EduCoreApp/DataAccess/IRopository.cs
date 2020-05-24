using System;
using System.Linq;
using System.Linq.Expressions;

namespace ECA.DataAccess
{
    public interface IRopository<T> where T : class
    {
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        void Insert(T entry);
        int SaveChanges();
    }
}
