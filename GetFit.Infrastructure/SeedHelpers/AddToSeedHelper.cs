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
    }
}
