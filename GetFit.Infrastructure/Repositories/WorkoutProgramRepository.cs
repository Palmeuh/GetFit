using GetFit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetFit.Infrastructure.Repositories
{
    public class WorkoutProgramRepository : GenericRepository<WorkoutProgram>
    {
        public WorkoutProgramRepository(GetFitContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<WorkoutProgram>> GetAll()
        {
            var workouts = await _context.WorkoutPrograms
                .Include(w => w.Workouts)
                .ThenInclude(e => e.Excercises)
                .ToListAsync();

            return workouts;
        }

        public override WorkoutProgram Edit(WorkoutProgram entity)
        {

            var workoutProgram = _context.WorkoutPrograms
                .FirstOrDefault(e => e.Id == entity.Id);

            workoutProgram.Name = entity.Name;
            workoutProgram.Description = entity.Description;
            

            return base.Edit(workoutProgram);

        }

        public override IQueryable<WorkoutProgram> GetAllAsQuery()
        {
            return _context.Set<WorkoutProgram>()
                .Include(w => w.Workouts)
                .ThenInclude(e => e.Excercises)
                .AsQueryable();
        }
    }
}
