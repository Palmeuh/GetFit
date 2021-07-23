using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public virtual async Task Add(T entity)
        { 
            // await Context.AddAsync(entity);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAllAsQuery()
        {
            return  _context.Set<T>().AsQueryable();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>()                
                .ToListAsync();
        }

        public virtual T Remove(T entity)
        {
            return _context.Remove(entity).Entity;
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<T> GetById(int? id)
        {
            return await _context.FindAsync<T>(id);
        }

        public virtual async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public virtual T Edit(T entity)
        {
            return  _context.Update(entity).Entity;
            
        }
    }
}
