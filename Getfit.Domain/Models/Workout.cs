using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetFit.Domain.Models
{
    public class Workout
    {
        public int Id { get; set; }
        [StringLength(70, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }
        [StringLength(130, MinimumLength = 5)]
        [Required]
        public string Description { get; set; }
        public List<Excercise> Excercises { get; set; }
        public List<WorkoutProgram> WorkoutPrograms { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
