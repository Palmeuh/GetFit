using GetFit.Domain.Models;
using GetFit.Infrastructure.DataReader;
using GetFit.Infrastructure.SeedHelpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetFit.Infrastructure
{
    public static class GetFitSeedData
    {
        private static GetFitContext _context;
        private const string _excerciseFilePath = @"C:\Users\fredr\source\repos\GetFit\GetFit.Infrastructure\DataForSeed\excercises.txt";
        private const int _minWorkouts = 5;
        private const int _maxWorkouts = 8;
        private const int _minExcercises = 6;
        private const int _maxExcercises = 10;

        public static async Task CreateInitialDatabaseAsync(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();
            _context = serviceScope.ServiceProvider.GetRequiredService<GetFitContext>();
            await _context.Database.EnsureCreatedAsync();

            if (!await _context.Excercise.AnyAsync())
            {
                await GenerateExcercises();
                await GenerateWorkouts();
                await GenerateWorkoutPrograms();
            }
        }


        private async static Task GenerateExcercises()
        {
            var excercises = await ExcerciseFileReader.ReadFile(_excerciseFilePath);

            var excercisesToAddToDb = new List<Excercise>();

            for (int i = 0; i < excercises.GetLength(0); i++)
            {
                var excercise = new Excercise
                {
                    Name = excercises[i, 0],
                    Description = "Some description for the excercise",
                    MuscleGroup = excercises[i, 1],
                    Category = AddToSeedHelper.SelectCategory(excercises[i,1])
                };
                excercisesToAddToDb.Add(excercise);
            }

            await _context.Excercise.AddRangeAsync(excercisesToAddToDb);
            await _context.SaveChangesAsync();
        }


        private async static Task GenerateWorkouts()
        {
            var excercises = await _context.Excercise.ToListAsync();
            var workoutsToAdd = new List<Workout>()
            {
                new Workout { Name = "Chest v1", Description = "Some other dummy text in the meantime", Excercises = AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Chest v2", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Shoulders v1", Description = "Some other dummy text in the meantime", Excercises = AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Back v1", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Balanced", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Lower Body", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Upper Body", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Cardio", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Fat Burn", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Arms", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Legs v2", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Core", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Shoulders v2", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Back v2", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
                new Workout { Name = "Legs v2", Description = "Some other dummy text in the meantime", Excercises =  AddToSeedHelper.AddToSeed(excercises,  _minExcercises, _maxExcercises) },
            };

            await _context.Workouts.AddRangeAsync(workoutsToAdd);
            await _context.SaveChangesAsync();
        }

        private static async Task GenerateWorkoutPrograms()
        {
            var workouts = await _context.Workouts.ToListAsync();

            var workoutProgramsToAdd = new List<WorkoutProgram>()
            {
                new WorkoutProgram {Name = "No Legs", Description = "Some other dummy text in the meantime", Workouts = AddToSeedHelper.AddToSeed(workouts,  _minWorkouts, _maxWorkouts)},
                new WorkoutProgram {Name = "Beach Boys", Description = "Some other dummy text in the meantime", Workouts = AddToSeedHelper.AddToSeed(workouts,  _minWorkouts, _maxWorkouts)},
                new WorkoutProgram {Name = "Disco Machine", Description = "Some other dummy text in the meantime", Workouts = AddToSeedHelper.AddToSeed(workouts,  _minWorkouts, _maxWorkouts)},
                new WorkoutProgram {Name = "Summer 2022", Description = "Some other dummy text in the meantime", Workouts = AddToSeedHelper.AddToSeed(workouts,  _minWorkouts, _maxWorkouts)},
                new WorkoutProgram {Name = "Super Size Me", Description = "Some other dummy text in the meantime", Workouts = AddToSeedHelper.AddToSeed(workouts,  _minWorkouts, _maxWorkouts)},
                new WorkoutProgram {Name = "Lean Machine", Description = "Some other dummy text in the meantime", Workouts = AddToSeedHelper.AddToSeed(workouts,  _minWorkouts, _maxWorkouts)},
                new WorkoutProgram {Name = "Chillout", Description = "Some other dummy text in the meantime", Workouts = AddToSeedHelper.AddToSeed(workouts,  _minWorkouts, _maxWorkouts)},
            };

            await _context.WorkoutPrograms.AddRangeAsync(workoutProgramsToAdd);
            await _context.SaveChangesAsync();
        }

    }
}
