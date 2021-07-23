using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GetFit.Infrastructure.DataReader
{
    public static class ExcerciseFileReader
    {
        public async static Task<string[,]> ReadFile(string fileUrl)
        {
            var text = await File.ReadAllLinesAsync(fileUrl);
            var filteredList = new List<string>();

            foreach (var line in text)
            {
                if (!filteredList.Contains(line))
                {
                    filteredList.Add(line);
                }
            }

            var counter = filteredList.Count();
            var textCount = filteredList.Count();

            string[,] array = new string[textCount, 2];

            for (int i = 0; i < filteredList.Count(); i++)
            {
                var tempArray = filteredList[i].Split(",");

                for (int j = 0; j < 2; j++)
                {
                    array[i, j] = tempArray[j];
                }
            }

            return array;
        }

    }
}

