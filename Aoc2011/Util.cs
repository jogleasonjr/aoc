using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aoc
{
    public static class Util
    {
        public static int[] GetIntsFromFile(string filename)
        {
            return File.ReadAllLines(@"data/" + filename).Select(s => Int32.Parse(s)).ToArray();
        }

        public static string[] GetStringsFromFile(string filename)
        {
            return File.ReadAllLines(@"data/" + filename);
        }

        public static string ReverseString(string s)
        {
            var stringArray = s.ToCharArray();
            Array.Reverse(stringArray);

            return new string(stringArray);
        }

        public static int[] ParseNumbersFromLine(string line, char splitChar)
        {
            return line.Split(splitChar, StringSplitOptions.RemoveEmptyEntries).Select(s => Int32.Parse(s)).ToArray();
        }
    }
}