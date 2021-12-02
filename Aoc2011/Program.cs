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

            var inputD2 = new string[] {"forward 5","down 5","forward 8","up 3","down 8","forward 2"};
            inputD2 = Util.GetStringsFromFile("day2.txt");

            Day2p1(inputD2);
            Day2p2(inputD2);
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