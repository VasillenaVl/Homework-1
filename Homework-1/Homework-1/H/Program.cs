using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace H
{
    class Program
    {
        static volatile bool findShortestFinished;
        static volatile bool findLongestFinished;
        static volatile bool findAverageFinished;
        static volatile bool findMostLeastCommon;
        static List<string> words = new List<string>();
        static volatile int countOfwords;
        static volatile string shortest;
        static volatile string longest;
        static volatile int average;
        static volatile List<string> mostCommon = new List<string>();
        static volatile List<string> leastCommon = new List<string>();
         


        static void Main(string[] args)
        {

            findShortestFinished = false;
            findLongestFinished = false;            
            findAverageFinished = false;            
            findMostLeastCommon = false;
            
            string text = File.ReadAllText("../../Book.txt");
           
            string[] multiArray = text.Split(new Char[] { ' ', ',', '.', '-', '!', '?', '*', ':',
                ';', '“', '„', '[', ']', '№', '/', '\\', '>', '<', '%', '&', '+', '«', '»', '\n', '\t', '\r'});

            
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

            Thread shortestFinder = new Thread(FindShortest);
            shortestFinder.Start();

            Thread lognestFinder = new Thread(FindLongest);
            lognestFinder.Start();

            Thread averageFinder = new Thread(FindAverage);
            averageFinder.Start();

            Thread mostLeastFinder = new Thread(FindFiveMostLeastCommon);
            mostLeastFinder.Start();

            while (!findShortestFinished || !findLongestFinished || !findAverageFinished || !findMostLeastCommon)
            {
                
            }

            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            double timeMS = timer.Elapsed.TotalMilliseconds;

            
            Console.WriteLine($"Time taken Multi threading: {timeMS}");

            
            shortestFinder.Join();
            shortestFinder = null;
            lognestFinder.Join();
            lognestFinder = null;
            averageFinder.Join();
            averageFinder = null;
            mostLeastFinder.Join();
            mostLeastFinder = null;

            Console.WriteLine($"The number of words: {countOfwords}");
            Console.WriteLine($"The shortest word: { shortest}");
            Console.WriteLine($"The longest word: {longest}");
            Console.WriteLine($"Average word length: {average}");

            Console.WriteLine("Five most common words: ");
            for (int m = 0; m < mostCommon.Count; m++)
            {
                Console.WriteLine(mostCommon[m]);
            }

            Console.WriteLine("Five least common words: ");
            for (int l = 0; l < mostCommon.Count; l++)
            {
                Console.WriteLine(leastCommon[l]);
            }

            Console.ReadLine();
            return;

        }
        static void FindShortest()
        {
            int countOfLetters;
            string shortestWord;

            countOfLetters = words[0].Length;
            shortestWord = words[0];
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Length < countOfLetters)
                {
                    shortestWord = words[i];
                    countOfLetters = words[i].Length;
                }
            }
            shortest = shortestWord;
            findShortestFinished = true;
        }

        static void FindLongest()
        {
            string longestWord;
            int countOfLetters;
            longestWord = words[0];
            countOfLetters = words[0].Length;
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Length > countOfLetters)
                {
                    longestWord = words[i];
                    countOfLetters = words[i].Length;
                }
            }

            longest = longestWord;
            findLongestFinished = true;
        }
        static void FindAverage()
        {
            int averageword;
            int countOfLetters;
            countOfLetters = 0;
            for (int a = 0; a < words.Count; a++)
            {
                countOfLetters += words[a].Length;
            }
            averageword = countOfLetters / countOfwords;

            average = averageword;
            findAverageFinished = true;
        }
        static void FindFiveMostLeastCommon()
        {
            var query = words.GroupBy(x => x.ToLower())
                         .Where(g => g.Count() > 0)
                         .ToDictionary(x => x.Key, y => y.Count());

            IOrderedEnumerable<KeyValuePair<string, int>> qDict = query.OrderBy(x => x.Value);
            //string name = qDict.ElementAt(0).Key;

            mostCommon.Add(qDict.ElementAt(Enumerable.Count(qDict) - 1).Key);
            mostCommon.Add(qDict.ElementAt(Enumerable.Count(qDict) - 2).Key);
            mostCommon.Add(qDict.ElementAt(Enumerable.Count(qDict) - 3).Key);
            mostCommon.Add(qDict.ElementAt(Enumerable.Count(qDict) - 4).Key);
            mostCommon.Add(qDict.ElementAt(Enumerable.Count(qDict) - 5).Key);

            leastCommon.Add(qDict.ElementAt(0).Key);
            leastCommon.Add(qDict.ElementAt(1).Key);
            leastCommon.Add(qDict.ElementAt(2).Key);
            leastCommon.Add(qDict.ElementAt(3).Key);
            leastCommon.Add(qDict.ElementAt(4).Key);

            findMostLeastCommon = true;
        }
    }
}
