using System.Collections.Generic;

namespace GetFit.Domain.Models
{
    public class WorkoutProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Workout> Workouts { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
