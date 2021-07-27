using GetFit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public override async Task<WorkoutProgram> GetById(int? id)
        {
            return await _context.WorkoutPrograms
                 .Where(wp => wp.Id == id)
                 .Include(w => w.Workouts)
                 .ThenInclude(e => e.Excercises)
                 .FirstOrDefaultAsync();
        }

        public override WorkoutProgram Edit(WorkoutProgram entity)
        {

            WorkoutProgram workoutProgram = _context.WorkoutPrograms
                .Include(w => w.Workouts)
                .ThenInclude(e => e.Excercises)
                .FirstOrDefault(e => e.Id == entity.Id);

            workoutProgram.Name = entity.Name;
            workoutProgram.Description = entity.Description;


            return base.EditAsync(workoutProgram);

        }

        public override async Task<IEnumerable<WorkoutProgram>> Find(Expression<Func<WorkoutProgram, bool>> predicate)
        {
            return await _context.Set<WorkoutProgram>()
                .Include(w => w.Workouts)
                .ThenInclude(e => e.Excercises)
                .Distinct()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync();
           
        }


    }
}
