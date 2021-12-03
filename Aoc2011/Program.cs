using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aoc
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputD1 = new int[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            inputD1 = Util.GetIntsFromFile("day1.txt");

            Day1p1(inputD1);
            Day1p2(inputD1);

            var inputD2 = new string[] { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };
            inputD2 = Util.GetStringsFromFile("day2.txt");

            Day2p1(inputD2);
            Day2p2(inputD2);

            var inputD3 = new string[] { "00100", "11110", "10110", "10111", "10101", "01111", "00111", "11100", "10000", "11001", "00010", "01010" };
            inputD3 = Util.GetStringsFromFile("day3.txt");
            Day3p1(inputD3);
            Day3p2(inputD3);
        }

        static void Day3p2(IEnumerable<string> binStrings)
        {
            var len = binStrings.First().Length;
            IEnumerable<string> o2Rating = binStrings.ToList();
            IEnumerable<string> co2Rating = binStrings.ToList();

            for (int i = 0; i < len; i++)
            {
                var onesCount = o2Rating.Where(s => s[i] == '1').Count();

                if (onesCount >= o2Rating.Count() / 2.0)
                {
                    o2Rating = o2Rating.Count() == 1 ? o2Rating : o2Rating.Where(s => s[i] == '1').ToList();
                }
                else
                {

                    o2Rating = o2Rating.Count() == 1 ? o2Rating : o2Rating.Where(s => s[i] == '0').ToList();
                }
            }

            for (int i = 0; i < len; i++)
            {
                var onesCount = co2Rating.Where(s => s[i] == '1').Count();

                if (onesCount >= co2Rating.Count() / 2.0)
                {
                    co2Rating = co2Rating.Count() == 1 ? co2Rating : co2Rating.Where(s => s[i] == '0').ToList();
                }
                else
                {
                    co2Rating = co2Rating.Count() == 1 ? co2Rating : co2Rating.Where(s => s[i] == '1').ToList();
                }
            }

            var o2RatingVal = Convert.ToInt32(o2Rating.Single(), fromBase: 2);
            var co2RatingVal = Convert.ToInt32(co2Rating.Single(), fromBase: 2);
            var lsRatingVal = o2RatingVal * co2RatingVal;

            Console.WriteLine($"Day3p2: o2RatingVal={o2RatingVal}, co2RatingVal={co2RatingVal}, power={lsRatingVal}");
        }

        static void Day3p1(IEnumerable<string> binStrings)
        {
            var len = binStrings.First().Length;
            var maj = string.Empty;
            var min = string.Empty;

            for (int i = 0; i < len; i++)
            {
                var onesCount = binStrings.Where(s => s[i] == '1').Count();

                if (onesCount > binStrings.Count() / 2)
                {
                    maj += "1";
                    min += "0";
                }
                else
                {
                    maj += "0";
                    min += "1";
                }
            }

            var gammaRate = Convert.ToInt32(maj, fromBase: 2);
            var epsilon = Convert.ToInt32(min, fromBase: 2);
            var power = gammaRate * epsilon;

            Console.WriteLine($"Day3p2: gammaRate={gammaRate}, epsilon={epsilon}, power={power}");
        }

        static void Day2p2(IEnumerable<string> movements)
        {
            int x = 0, y = 0, a = 0;

            foreach (var movement in movements)
            {
                var dir = movement.Split(' ')[0];
                var dist = Int32.Parse(movement.Split(' ')[1]);

                switch (dir)
                {
                    case "forward":
                        x += dist;
                        y += a * dist;
                        break;
                    case "up":
                        a -= dist;
                        break;
                    case "down":
                        a += dist;
                        break;
                }
            }

            Console.WriteLine($"Day2p2: {x * y}");
        }

        static void Day2p1(IEnumerable<string> movements)
        {
            int x = 0, y = 0;

            foreach (var movement in movements)
            {
                var dir = movement.Split(' ')[0];
                var dist = Int32.Parse(movement.Split(' ')[1]);

                switch (dir)
                {
                    case "forward":
                        x += dist;
                        break;
                    case "up":
                        y -= dist;
                        break;
                    case "down":
                        y += dist;
                        break;
                }
            }

            Console.WriteLine($"Day2p1: {x * y}");
        }

        static void Day1p2(int[] input)
        {
            int increasingCount = 0;
            int prevSum = 0;
            for (int i = 1; i < input.Length - 1; i++)
            {
                var sum = input.Skip(i - 2).Take(3).Sum();
                if (sum > prevSum)
                {
                    increasingCount++;
                }

                prevSum = sum;
            }

            Console.WriteLine($"Day1p2: {increasingCount}");
        }

        static void Day1p1(IEnumerable<int> input)
        {
            var arr = input.ToArray();

            int increasingCount = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > arr[i - 1])
                {
                    increasingCount++;
                }
            }

            Console.WriteLine($"Day1p1: {increasingCount}");
        }
    }
}