using System.Collections.Generic;
using System.Linq;

namespace GetFit.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
        T Add(T entity);
        T Edit(T entity);
        T Remove(T entity);
        void SaveChanges();
        IQueryable<T> GetAllAsQuery();
    }
}
