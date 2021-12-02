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
    }
}