using GetFit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Infrastructure
{
    public class GetFitSeedData
    {
        private readonly GetFitContext _context;

        public GetFitSeedData(GetFitContext context)
        {
            _context = context;
        }

        public async Task CreateInitialDatabaseAsync()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            List<Excercise> excercises = new()
            {
                new Excercise { Name = "Test1", MuscleGroup = "Abs", Description = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTest" },
                new Excercise { Name = "Test2", MuscleGroup = "Legs", Description = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTest" },
                new Excercise { Name = "Test3", MuscleGroup = "Chest", Description = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTest" }

            };

            await _context.Excercise.AddRangeAsync(excercises);
            await _context.SaveChangesAsync();
        }
    }
}
