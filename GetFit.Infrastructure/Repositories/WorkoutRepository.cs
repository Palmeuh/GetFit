using GetFit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public override Workout Edit(Workout entity)
        {
            var workout = _context.Workouts
                .Single(w => w.Id == entity.Id);

            workout.Name = entity.Name;
            workout.Description = entity.Description;


            return base.Edit(workout);
        }
    }
}
