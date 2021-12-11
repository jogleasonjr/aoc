using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace Aoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Day10p1();
            Day10p2();
            return;

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

            Day4p1("day4.txt");
            Day4p2("day4.txt");

            Day5p1("day5.txt");

            Day6p1();
            Day6p2();

            Day7p1();
            Day7p2();

            Day8p1();
            Day8p2();

            Day9p1();
            Day9p2();
        }


        static void Day10p2()
        {
            var parser = new ChunkParser();
            var points = parser.GetPointsFromFileP2("day10.txt");

            Console.WriteLine($"Day10p2 points: {points}");
        }

        static void Day10p1()
        {
            var parser = new ChunkParser();
            int points = parser.GetPointsFromFile("day10.txt");

            Console.WriteLine($"Day10p1 points: {points}");
        }
        static void Day9p2()
        {
            var m = Util.GetPadded2DIntArrayFromFile("day9.txt", 99);
            var sizes = new List<int>();

            for (int r = 1; r < m.Length - 1; r++)
            {
                var row = m[r];

                for (int c = 1; c < row.Length - 1; c++)
                {
                    var cell = m[r][c];
                    var up = m[r - 1][c];
                    var down = m[r + 1][c];
                    var left = m[r][c - 1];
                    var right = m[r][c + 1];

                    if (cell < up &&
                       cell < down &&
                       cell < left &&
                       cell < right)
                    {
                        // bottom of basin, search outward
                        var cells = new List<Cell>();
                        var hits = Cell.FindHigherNeighbors(m, r, c, cells).ToArray();
                        sizes.Add(hits.Count() + 1);
                    }
                }
            }

            var top3 = sizes.OrderByDescending(x => x).Take(3).ToArray();
            var risk = top3[0] * top3[1] * top3[2];

            Console.WriteLine($"Day9p2 risk: {risk}");
        }

        static void Day9p1()
        {
            var m = Util.GetPadded2DIntArrayFromFile("day9.txt", 99);
            int risk = 0;

            for (int r = 1; r < m.Length - 1; r++)
            {
                var row = m[r];

                for (int c = 1; c < row.Length - 1; c++)
                {
                    var cell = m[r][c];
                    var up = m[r - 1][c];
                    var down = m[r + 1][c];
                    var left = m[r][c - 1];
                    var right = m[r][c + 1];

                    if (cell < up &&
                       cell < down &&
                       cell < left &&
                       cell < right)
                    {
                        risk += cell + 1;
                    }
                }
            }

            Console.WriteLine($"Day9p1 risk: {risk}");
        }

        static void Day8p2()
        {
            //var input = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf";
            var input = Util.GetStringsFromFile("day8.txt");
            int sum = 0;

            foreach (var line in input)
            {
                var signalText = line.Split('|')[0];
                var signal = signalText.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                var outputText = line.Split('|')[1];
                var outputs = outputText.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

                var one = Util.FindAndRemove(signal, s => s.Length == 2);
                var four = Util.FindAndRemove(signal, s => s.Length == 4);
                var seven = Util.FindAndRemove(signal, s => s.Length == 3);
                var eight = Util.FindAndRemove(signal, s => s.Length == 7);

                var aCount = 8;
                var bCount = 6;
                var cCount = 8;
                var dCount = 7;
                var eCount = 4;
                var fCount = 9;
                var gCount = 7;

                var charCountDict = Util.GetCharInstanceCount(signalText);

                var a = seven.Single(s => !one.Contains(s));
                var b = charCountDict.Single(x => x.Value == bCount).Key;
                var c = charCountDict.Single(x => x.Value == cCount && x.Key != a).Key;
                var e = charCountDict.Single(x => x.Value == eCount).Key;
                var f = charCountDict.Single(x => x.Value == fCount).Key;
                var g = charCountDict.Single(x => x.Value == gCount && !four.Contains(x.Key)).Key;
                var d = charCountDict.Single(x => x.Value == dCount && x.Key != g).Key;

                var zero = String.Join(string.Empty, a, b, c, e, f, g);
                var two = String.Join(string.Empty, a, c, d, e, g);
                var three = String.Join(string.Empty, a, c, d, f, g);
                var five = String.Join(string.Empty, a, b, d, f, g);
                var six = String.Join(string.Empty, a, b, d, e, f, g);
                var nine = String.Join(string.Empty, a, b, c, d, f, g);

                var outputDictionary = new Dictionary<string, int>
                {
                   {zero, 0}, {one, 1}, {two, 2}, {three, 3}, {four, 4},
                   {five, 5}, {six, 6}, {seven, 7}, {eight, 8}, {nine, 9}
                };

                var outputValue = string.Empty;
                foreach (var output in outputs)
                {
                    var digit = outputDictionary.Single(s => Util.IsAnagram(s.Key, output)).Value;
                    outputValue += digit;
                }

                sum += Int32.Parse(outputValue);
            }

            Console.WriteLine($"Day8p2 sum: {sum}");
        }

        static void Day8p1()
        {
            var input = Util.GetStringsFromFile("day8.txt");

            var uniqueSizes = new int[] { 2, 3, 4, 7 };
            int uniqueCount = 0;

            foreach (var line in input)
            {
                var signal = line.Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                var output = line.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

                uniqueCount += output.Count(s => uniqueSizes.Contains(s.Length));
            }


            Console.WriteLine($"Day8p1 uniqueCount: {uniqueCount}");
        }

        static void Day7p2()
        {
            var input = new int[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
            input = Util.RegexGetIntsFromFile("day7.txt");

            var inputMin = input.Min();
            var inputMax = input.Max();

            int minDistance = 0, minDistanceSum = Int32.MaxValue;

            for (int i = 0; i <= inputMax; i++)
            {
                int sum = 0;

                foreach (var x in input)
                {
                    var distance = Math.Abs(x - i);
                    sum += (int)((distance / 2.0) * (distance + 1.0));
                }

                if (sum < minDistanceSum)
                {
                    minDistanceSum = sum;
                    minDistance = i;
                }
            }

            Console.WriteLine($"Day7p2 minDistance: {minDistance} | minDistanceSum: {minDistanceSum}");
        }

        static void Day7p1()
        {
            var input = new int[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
            input = Util.RegexGetIntsFromFile("day7.txt");

            var inputMin = input.Min();
            var inputMax = input.Max();

            int minDistance = 0, minDistanceSum = Int32.MaxValue;

            for (int i = 0; i <= inputMax; i++)
            {
                int sum = 0;

                foreach (var x in input)
                {
                    sum += Math.Abs(x - i);
                }

                if (sum < minDistanceSum)
                {
                    minDistanceSum = sum;
                    minDistance = i;
                }
            }

            Console.WriteLine($"Day7p1 minDistance: {minDistance} | minDistanceSum: {minDistanceSum}");
        }

        static void Day6p2()
        {
            var inputD6 = new int[] { 3, 4, 3, 1, 2 };
            inputD6 = Util.RegexGetIntsFromFile("day6.txt");

            var school = new LanternFishSchool(inputD6);
            school.Cycle(256, true);

            ulong yuge = 0;
            foreach (var fish in school.Bag)
            {
                yuge += (ulong)fish.Clones;
            }

            Console.WriteLine($"Day6p2: {yuge}");
        }

        static void Day6p1()
        {
            var inputD6 = new int[] { 3, 4, 3, 1, 2 };
            inputD6 = Util.RegexGetIntsFromFile("day6.txt");

            var school = new LanternFishSchool(inputD6);
            school.Cycle(80, true);

            ulong yuge = 0;
            foreach (var fish in school.Bag)
            {
                yuge += (ulong)fish.Clones;
            }

            Console.WriteLine($"Day6p1: {yuge}");
        }

        static void Day5p1(string filename)
        {
            var lines = LineCollection.ParseFile(filename);
            //lines.Print();
            Console.WriteLine($"Day5p1: {lines.IntersectionCount}");
        }

        static void Day4p2(string filename)
        {
            var game = BingoGame.ParseFile(filename);

            Console.WriteLine($"Day4p1: {game.FindLastWinner()}");
        }
        static void Day4p1(string filename)
        {
            var game = BingoGame.ParseFile(filename);

            Console.WriteLine($"Day4p1: {game.FindWinner()}");
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