using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
       IEnumerable<T> GetAll();
       T GetById(int? id);
       T Add(T entity);
       T Edit (T entity);
       T Remove (T entity);
       void SaveChanges();
    }
}
