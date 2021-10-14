using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1
{
    class Program
    {

       
        static void Main(string[] args)
        {
            int countOfwords;
            string shortest;
            string longest;
            int average;

            List<string> mostCommon = new List<string>();
            List<string> leastCommon = new List<string>();


            string text = File.ReadAllText("../../Book.txt");
           
            string[] multiArray = text.Split(new Char[] { ' ', ',', '.', '-', '!', '?', '*', ':',
                ';', '“', '„', '[', ']', '№', '/', '\\', '>', '<', '%', '&', '+', '«', '»', '\n', '\t', '\r'});

            List<string> words = new List<string>();
            foreach (string element in multiArray)
            {
                if (element.Length > 1)
                {
                    words.Add(element);
                }

            }
            countOfwords = words.Count;

            var timer = new Stopwatch();
            timer.Start();


            Console.WriteLine($"The number of words: {countOfwords}");

            shortest = FindShortest(words);
            Console.WriteLine($"The shortest word: {shortest}");
            longest = FindLongest(words);
            Console.WriteLine($"The longest word: {longest}");



            average = FindAverage(words, countOfwords);
            Console.WriteLine($"Average word length: { average}");

            var query = words.GroupBy(x => x.ToLower())
                          .Where(g => g.Count() > 0)
                          .ToDictionary(x => x.Key, y => y.Count());

            IOrderedEnumerable<KeyValuePair<string, int>> qDict = query.OrderBy(x => x.Value);
           

            mostCommon.Add(qDict.ElementAt(Enumerable.Count(qDict) - 1).Key);
            mostCommon.Add(qDict.ElementAt(Enumerable.Count(qDict) - 2).Key);
            mostCommon.Add(qDict.ElementAt(Enumerable.Count(qDict) - 3).Key);
            mostCommon.Add(qDict.ElementAt(Enumerable.Count(qDict) - 4).Key);
            mostCommon.Add(qDict.ElementAt(Enumerable.Count(qDict) - 5).Key);

            Console.WriteLine("Five most common words: ");


            for (int m = 0; m < mostCommon.Count; m++)
            {
                Console.WriteLine($"{mostCommon[m]}");
            }

            leastCommon.Add(qDict.ElementAt(0).Key);
            leastCommon.Add(qDict.ElementAt(1).Key);
            leastCommon.Add(qDict.ElementAt(2).Key);
            leastCommon.Add(qDict.ElementAt(3).Key);
            leastCommon.Add(qDict.ElementAt(4).Key);

            Console.WriteLine("Five least common words: ");
            for (int l = 0; l < mostCommon.Count; l++)
            {
                Console.WriteLine($"{leastCommon[l]}");
            }

            Console.ReadLine();
            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            double timeMS = timer.Elapsed.TotalMilliseconds;


            Console.WriteLine($"Time taken: {timeMS}");

            return;

        }
        static string FindShortest(List<string> words)
        {
            int countOfLetters;
            string shortestWord;

            countOfLetters = words[0].Length;
            shortestWord = words[0];


            var timer = new Stopwatch();
            timer.Start();


            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Length < countOfLetters)
                {
                    shortestWord = words[i];
                    countOfLetters = words[i].Length;
                }
            }

            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            double timeMS = timer.Elapsed.TotalMilliseconds;


            Console.WriteLine($"Time taken: {timeMS}");
            return shortestWord;


        }
        static string FindLongest(List<string> words)
        {
            string longestWord;
            int countOfLetters;
            longestWord = words[0];
            countOfLetters = words[0].Length;
            var timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Length > countOfLetters)
                {
                    longestWord = words[i];
                    countOfLetters = words[i].Length;
                }
            }
            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            double timeMS = timer.Elapsed.TotalMilliseconds;


            Console.WriteLine($"Time taken: {timeMS}");

            return longestWord;
        }
        static int FindAverage(List<string> words, int countOfwords)
        {
            int average;
            int countOfLetters;
            countOfLetters = 0;
            var timer = new Stopwatch();
            timer.Start();

            for (int a = 0; a < words.Count; a++)
            {
                countOfLetters += words[a].Length;
            }
            average = countOfLetters / countOfwords;

            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            double timeMS = timer.Elapsed.TotalMilliseconds;


            Console.WriteLine($"Time taken: {timeMS}");

            return average;

        }


    }
}
