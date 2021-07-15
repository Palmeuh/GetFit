using GetFit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GetFit.Infrastructure.Repositories
{
    public class WorkoutProgramRepository : GenericRepository<WorkoutProgram>
    {
        public WorkoutProgramRepository(GetFitContext context) : base(context)
        {

        }

        public override IEnumerable<WorkoutProgram> GetAll()
        {
            var workouts = _context.WorkoutPrograms
                .Include(w => w.Workouts)
                .ThenInclude(e => e.Excercises)
                .ToList();

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
    }
}
