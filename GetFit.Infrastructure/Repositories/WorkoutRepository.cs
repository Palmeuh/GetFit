using GetFit.Domain.Models;
using System.Linq;

namespace GetFit.Infrastructure.Repositories
{
    public class WorkoutRepository : GenericRepository<Workout>
    {

        public WorkoutRepository(GetFitContext context) : base(context)
        {

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
