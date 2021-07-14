using GetFit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Infrastructure.Repositories
{
    public class WorkoutProgramRepository : GenericRepository<WorkoutProgram>
    {
        public WorkoutProgramRepository(GetFitContext context) : base(context)
        {

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
