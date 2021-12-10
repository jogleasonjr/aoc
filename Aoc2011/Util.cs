using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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

        public static int[] RegexGetIntsFromFile(string filename)
        {
            return Regex.Matches(File.ReadAllText(@"data/" + filename), @"\d+").Select(m => Int32.Parse(m.Value)).ToArray();
        }

        public static string[] GetStringsFromFile(string filename)
        {
            return File.ReadAllLines(@"data/" + filename);
        }

        public static int[][] GetPadded2DIntArrayFromFile(string filename, int padNum)
        {
            var lines = File.ReadAllLines(@"data/" + filename);
            var m = new int[lines.Length + 2][];

            m[0] = Enumerable.Range(0, lines[0].Length + 2).Select(x => padNum).ToArray();
            m[lines.Length + 1] = Enumerable.Range(0, lines[0].Length + 2).Select(x => padNum).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var row = new int[line.Length + 2];
                row[0] = padNum;
                row[line.Length + 1] = padNum;
                for (int j = 1; j < line.Length + 1; j++)
                {
                    row[j] = Int32.Parse(line[j - 1].ToString());
                }

                m[i + 1] = row;
            }

            return m;
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

        public static bool IsAnagram(string a, string b)
        {
            return a.ToList().OrderBy(x => x).SequenceEqual(b.ToList().OrderBy(x => x));
        }

        public static string FindAndRemove(List<string> strings, Func<string, bool> func)
        {
            var item = strings.Where(func).Single();
            strings.Remove(item);
            return item;
        }

        public static Dictionary<char, int> GetCharInstanceCount(string s)
        {
            var d = new Dictionary<char, int>();

            foreach (var c in s)
            {
                if (!d.ContainsKey(c))
                {
                    d.Add(c, 0);
                }

                d[c]++;
            }

            return d;
        }
    }
}