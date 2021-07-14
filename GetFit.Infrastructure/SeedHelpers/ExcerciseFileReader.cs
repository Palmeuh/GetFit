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
            var counter = text.Count();
            var textCount = text.Count();

            string[,] array = new string[textCount, 2];

            for (int i = 0; i < text.Count(); i++)
            {
                var tempArray = text[i].Split(",");

                for (int j = 0; j < 2; j++)
                {
                    array[i, j] = tempArray[j];
                }
            }

            return array;
        }

    }
}

