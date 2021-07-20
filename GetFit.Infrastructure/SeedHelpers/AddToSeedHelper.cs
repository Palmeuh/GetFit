using GetFit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Infrastructure.SeedHelpers
{
    public static class AddToSeedHelper
    {
        private static readonly Random _random = new();

        public static List<T> AddToSeed<T>(List<T> list,
            int minToAdd, int maxToAdd)
        {
            var listToReturn = new List<T>();

            var minToChooseFrom = 0;
            var maxToChooseFrom = list.Count;

            var numberOfExcercisesToAdd = _random.Next(minToAdd, maxToAdd);

            for (int i = 0; i < numberOfExcercisesToAdd; i++)
            {
                listToReturn.Add(list[_random.Next(minToChooseFrom, maxToChooseFrom)]);
            }
            return listToReturn;
        }

        internal static Category SelectCategory(string v)
        {
            switch (v)
            {
                case "abdominals":
                    return Category.Core;

                case "adductors":
                    return Category.Legs;

                case "biceps":
                    return Category.Biceps;

                case "quadriceps":
                    return Category.Legs;

                case "shoulders":
                    return Category.Shoulders;

                case "chest":
                    return Category.Chest;

                case "lower back":
                    return Category.Back;

                case "middle back":
                    return Category.Back;

                case "triceps":
                    return Category.Triceps;

                case "hamstrings":
                    return Category.Legs;

                case "lats":
                    return Category.Back;

                case "traps":
                    return Category.Back;

                case "calves":
                    return Category.Legs;

                case "forearms":
                    return Category.Biceps;

                case "neck":
                    return Category.Back;

                case "glutes":
                    return Category.Legs;



                default:
                    return Category.None;

            }
        }
    }
}
