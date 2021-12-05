using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace Aoc
{
    public class Line
    {
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }

        public Line(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
    }

    public class LineCollection
    {
        public IEnumerable<Line> Lines { get; }
        public int MaxX { get; }
        public int MaxY { get; }

        public int IntersectionCount { get; }

        public int[,] Matrix { get; }

        public LineCollection(IEnumerable<Line> lines)
        {
            Lines = lines;
            MaxX = Math.Max(lines.Max(l => l.X1), lines.Max(l => l.X2)) + 1;
            MaxY = Math.Max(lines.Max(l => l.Y1), lines.Max(l => l.Y2)) + 1;

            var m2 = new int[MaxX + 1, MaxY + 1];

            foreach (var line in Lines)
            {
                if (line.X1 == line.X2 || line.Y1 == line.Y2)
                {
                    // draw straight lines
                    var minx = Math.Min(line.X1, line.X2);
                    var miny = Math.Min(line.Y1, line.Y2);
                    var maxx = Math.Max(line.X1, line.X2);
                    var maxy = Math.Max(line.Y1, line.Y2);

                    for (int x = minx; x <= maxx; x++)
                    {
                        for (int y = miny; y <= maxy; y++)
                        {
                            m2[x, y]++;
                        }
                    }
                }
                else
                {
                    // draw diagonal lines
                    if (line.X1 > line.X2)
                    {
                        if (line.Y1 > line.Y2)
                        {
                            int y = line.Y1;
                            for (int x = line.X1; x >= line.X2; x--, y--)
                            {
                                m2[x, y]++;
                            }
                        }
                        else
                        {
                            int y = line.Y1;
                            for (int x = line.X1; x >= line.X2; x--, y++)
                            {
                                m2[x, y]++;
                            }
                        }
                    }
                    else
                    {
                        if (line.Y1 > line.Y2)
                        {
                            int y = line.Y1;
                            for (int x = line.X1; x <= line.X2; x++, y--)
                            {
                                m2[x, y]++;
                            }
                        }
                        else
                        {
                            int y = line.Y1;
                            for (int x = line.X1; x <= line.X2; x++, y++)
                            {
                                m2[x, y]++;
                            }
                        }
                    }
                }
            }

            foreach (var i in m2)
            {
                if (i > 1)
                {
                    IntersectionCount++;
                }
            }

            Matrix = m2;
        }


        public void Print()
        {
            for (int y = 0; y < MaxY; y++)
            {
                var text = "";
                for (int x = 0; x < MaxX; x++)
                {
                    text += Matrix[x, y];

                }

                Console.WriteLine($"[{y}] {text}");
            }
        }
        public static LineCollection ParseFile(string filename)
        {
            var textLines = Util.GetStringsFromFile(filename);
            var lines = new List<Line>();

            foreach (var textLine in textLines)
            {
                var values = Regex.Matches(textLine, @"\d+").Select(m => Int32.Parse(m.Value)).ToArray();
                lines.Add(new Line(values[0], values[1], values[2], values[3]));
            }

            return new LineCollection(lines);
        }
    }
}