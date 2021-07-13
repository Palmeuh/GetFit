using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected GetFitContext _context;

        protected GenericRepository(GetFitContext context)
        {
            _context = context;
        }

        public virtual T Add(T entity)
        {
            return _context.Add(entity).Entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>()
                .ToList();
        }

        public virtual T Remove(T entity)
        {
            return _context.Remove(entity).Entity;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToList();
        }

        public virtual T GetById(int? id)
        {
            return _context.Find<T>(id);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual T Edit(T entity)
        {
            return _context.Update(entity).Entity;
        }
    }
}
