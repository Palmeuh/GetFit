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
    }
}
