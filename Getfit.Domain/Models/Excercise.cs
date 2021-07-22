using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetFit.Domain.Models
{
    public class Excercise
    {
        public int Id { get; set; }
        [StringLength(70, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }        
        public Category Category { get; set; }
        [StringLength(130, MinimumLength = 5)]
        [Required]
        public string Description { get; set; }
        public List<Workout> Workouts { get; set; }

    }

    public enum Category
    {
        Biceps = 1,
        Back = 2,
        Chest = 3,
        Core = 4,
        Legs = 5,
        Triceps = 6,
        Shoulders = 7,
        None = 8
    }
}
