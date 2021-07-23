using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetFit.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int? id);
        Task Add(T entity);
        T Edit(T entity);
        T Remove(T entity);
        Task SaveChanges();
        IQueryable<T> GetAllAsQuery();
    }
}
