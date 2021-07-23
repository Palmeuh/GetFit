using GetFit.Domain.Models;
using GetFit.Infrastructure.SearchSortFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Web.ViewModels
{
    public class AddExcerciseToWorkoutViewModel
    {
        public PaginatedList<Excercise> PaginatedList { get; set; }
        public Workout Workout { get; set; }
    }
}
