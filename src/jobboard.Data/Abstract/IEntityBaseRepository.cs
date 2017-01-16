using jobboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jobboard.Data.Abstract
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new ()
    {
        int Count();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void Commit();
    }
}
