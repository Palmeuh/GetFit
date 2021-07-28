using GetFit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Infrastructure.Repositories
{
    public class WorkoutPlanRepository : GenericRepository<WorkoutPlan>
    {
        public WorkoutPlanRepository(GetFitContext context) : base(context)
        {

        }

        public override async Task<WorkoutPlan> GetById(int? id)
        {
            return await _context.WorkoutPlans
                .Where(wp => wp.Id == id)
                .Include(wpr => wpr.WorkoutProgram)
                .ThenInclude(w => w.Workouts)
                .ThenInclude(e => e.Excercises)
                .FirstOrDefaultAsync();
        }

    }
}
