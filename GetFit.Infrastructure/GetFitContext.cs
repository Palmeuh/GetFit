using Microsoft.EntityFrameworkCore;
using GetFit.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GetFit.Infrastructure
{
    public class GetFitContext : IdentityDbContext<ApplicationUser>
    {
        public GetFitContext(DbContextOptions<GetFitContext> options)
            : base(options)
        {
        }



        public DbSet<Excercise> Excercise { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
    }
}
