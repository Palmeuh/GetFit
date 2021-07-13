using System.Collections.Generic;

namespace GetFit.Domain.Models
{
    public class Excercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  MuscleGroup { get; set; }
        public string Description { get; set; }
        public List<Workout> Workouts { get; set; }

    }
}
