using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace Aoc
{
    public class ChunkParser
    {
        private static Dictionary<char, char> Pairs { get; } = new Dictionary<char, char>
        {
            {'(', ')'}, {'[', ']'}, {'{', '}'}, {'<', '>'}
        };
        private static IEnumerable<char> OpenChars => Pairs.Keys;
        private static IEnumerable<char> CloseChars => Pairs.Values;

        private static string RemoveOpenChars(string s)
        {
            var removed = s;
            foreach (var openChar in OpenChars)
            {
                removed = removed.Replace(openChar.ToString(), string.Empty);
            }

            return removed;
        }

        private static bool IsAllOpenChars(string s) => s.All(OpenChars.Contains);

        public ulong GetPointsFromFileP2(string filename)
        {
            var lines = Util.GetStringsFromFile(filename);
            List<ulong> pointsList = new List<ulong>();

            foreach (var line in lines)
            {
                var lastReduced = string.Empty;
                var reduced = line;

                while (lastReduced != reduced)
                {
                    lastReduced = reduced;

                    foreach (var kvp in Pairs)
                    {
                        reduced = reduced.Replace(kvp.Key.ToString() + kvp.Value.ToString(), string.Empty);
                    }
                }

                if (string.IsNullOrEmpty(reduced))
                {
                    // valid and complete
                }
                else if (IsAllOpenChars(reduced))
                {
                    // valid but incomplete

                    ulong points = 0;

                    for (int c = reduced.Length - 1; c >= 0; c--)
                    {
                        points *= 5;

                        var s = reduced[c];

                        switch (s)
                        {
                            case '(':
                                points += 1;
                                break;
                            case '[':
                                points += 2;
                                break;
                            case '{':
                                points += 3;
                                break;
                            case '<':
                                points += 4;
                                break;
                        }
                    }



                    pointsList.Add(points);
                }
                else
                {
                    // closing chars are invalid
                }
            }

            var orderedPoints = pointsList.OrderBy(p => p).ToArray();
            var middleIndex = (int)Math.Floor(pointsList.Count / 2.0);
            var middle = orderedPoints[middleIndex];

            return middle;
        }
        public int GetPointsFromFile(string filename)
        {
            var lines = Util.GetStringsFromFile(filename);
            var points = 0;

            foreach (var line in lines)
            {
                var lastReduced = string.Empty;
                var reduced = line;

                while (lastReduced != reduced)
                {
                    lastReduced = reduced;

                    foreach (var kvp in Pairs)
                    {
                        reduced = reduced.Replace(kvp.Key.ToString() + kvp.Value.ToString(), string.Empty);
                    }
                }

                if (string.IsNullOrEmpty(reduced))
                {
                    // valid and complete
                }
                else if (IsAllOpenChars(reduced))
                {
                    // valid but incomplete
                }
                else
                {
                    // closing chars are invalid
                    var firstClosingChar = reduced.First(c => CloseChars.Contains(c));

                    switch (firstClosingChar)
                    {
                        case ')':
                            points += 3;
                            break;
                        case ']':
                            points += 57;
                            break;
                        case '}':
                            points += 1197;
                            break;
                        case '>':
                            points += 25137;
                            break;
                    }
                }
            }

            return points;
        }
    }
}