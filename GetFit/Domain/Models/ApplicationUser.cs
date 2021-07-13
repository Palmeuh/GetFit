using Microsoft.AspNetCore.Identity;

namespace GetFit.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; }
    }
}
