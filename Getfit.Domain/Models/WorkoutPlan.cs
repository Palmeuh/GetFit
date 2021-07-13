using System.Collections.Generic;

namespace GetFit.Domain.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public WorkoutPlan WorkoutProgram { get; set; }
        public List<Workout> Workouts { get; set; }
    }
}
