using GetFit.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Infrastructure
{
    public static class GetFitSeedData 
    {
        

      

        public static  async Task CreateInitialDatabaseAsync(IApplicationBuilder app)
        {

            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<GetFitContext>();

                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                List<Excercise> excercises = new()
                {
                    new Excercise { Name = "Test1", MuscleGroup = "Abs", Description = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTest" },
                    new Excercise { Name = "Test2", MuscleGroup = "Legs", Description = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTest" },
                    new Excercise { Name = "Test3", MuscleGroup = "Chest", Description = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTest" }

                };

                await context.Excercise.AddRangeAsync(excercises);
                await context.SaveChangesAsync();
            }
            
        }
    }
}
