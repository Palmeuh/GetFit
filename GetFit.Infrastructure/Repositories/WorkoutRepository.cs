using GetFit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GetFit.Infrastructure.Repositories
{
    public class WorkoutRepository : GenericRepository<Workout>
    {

        public WorkoutRepository(GetFitContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Workout>> GetAll()
        {
            var workouts = await _context.Workouts
                .Include(e => e.Excercises)
                .ToListAsync();

            return workouts;
        }

        public override Workout EditAsync(Workout entity)
        {
            var workout =   _context.Workouts
                .FirstOrDefault(w => w.Id == entity.Id);

            workout.Name = entity.Name;
            workout.Description = entity.Description;


            return base.EditAsync(workout);
        }

        public override async Task<Workout> GetById(int? id)
        {
            var workout = await _context.Workouts
                .Where(w => w.Id == id)
                .Include(e => e.Excercises)
                .FirstOrDefaultAsync();

            return workout;
        }

       

        public override async Task<IEnumerable<Workout>> Find(Expression<Func<Workout, bool>> predicate)
        {

            return await _context.Set<Workout>()
                .Include(e => e.Excercises)
                .Distinct()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
