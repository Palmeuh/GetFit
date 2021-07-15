using System.Collections.Generic;

namespace GetFit.Domain.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Excercise> Excercises { get; set; }
        public List<WorkoutProgram> WorkoutPrograms { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
