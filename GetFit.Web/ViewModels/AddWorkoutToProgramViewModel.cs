using GetFit.Domain.Models;
using GetFit.Infrastructure.SearchSortFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Web.ViewModels
{
    public class AddWorkoutToProgramViewModel
    {
        public PaginatedList<Workout> PaginatedList { get; set; }
        public WorkoutProgram WorkoutProgram { get; set; }
    }
}
