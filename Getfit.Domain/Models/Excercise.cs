using System.Collections.Generic;

namespace GetFit.Domain.Models
{
    public class Excercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public List<Workout> Workouts { get; set; }
        public string MuscleGroup { get; set; }

    }

    public enum Category
    {
        Biceps = 1,
        Back = 2,
        Chest = 3,
        Core = 4,
        Legs = 5,
        Triceps = 6,
        Shoulders = 7
    }
}
