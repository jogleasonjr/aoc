using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace Aoc
{
    public class Cell
    {
        public int R { get; }
        public int C { get; }
        public int V { get; }

        public Cell(int r, int c, int v)
        {
            R = r;
            C = c;
            V = v;
        }

        public override string ToString()
        {
            return $"{R},{C},{V}";
        }

        public static List<Cell> FindHigherNeighbors(int[][] m, int r, int c, List<Cell> list)
        {
            //var list = new List<Cell>();

            Func<Cell, bool> add = new Func<Cell, bool>(cell =>
            {
                if (cell.V < 9 && !list.Any(c => c.ToString() == cell.ToString()))
                {
                    list.Add(cell);
                    return true;
                }

                return false;
            });

            Action<List<Cell>> addRange = new Action<List<Cell>>(cells =>
            {
                cells.ForEach(c => add(c));
            });

            var value = m[r][c];
            var cell = new Cell(r, c, value);


            if (value > 8)
            {
                return list;
            }

            // up
            if (r > 1 && value <= m[r - 1][c])
            {
                if (add(new Cell(r - 1, c, m[r - 1][c])))
                {
                    addRange(FindHigherNeighbors(m, r - 1, c, list));
                }
            }

            // down
            if (r < m.Length - 1 && value <= m[r + 1][c])
            {
                if (add(new Cell(r + 1, c, m[r + 1][c])))
                {
                    addRange(FindHigherNeighbors(m, r + 1, c, list));
                }
            }

            // left
            if (c > 1 && value <= m[r][c - 1])
            {
                if (add(new Cell(r, c - 1, m[r][c - 1])))
                {
                    addRange(FindHigherNeighbors(m, r, c - 1, list));
                }
            }

            // right
            if (c < m[r].Length - 1 && value <= m[r][c + 1])
            {
                if (add(new Cell(r, c + 1, m[r][c + 1])))
                {
                    addRange(FindHigherNeighbors(m, r, c + 1, list));
                }
            }

            return list;
        }

        public static int FindBlobSize(int[][] m, int r, int c)
        {
            int blobSize = 0;

            var cell = m[r][c];

            if (cell > 8)
            {
                return 0;
            }

            // up
            if (r > 1 && cell <= m[r - 1][c])
            {
                blobSize += 1 + FindBlobSize(m, r - 1, c);
            }

            // down
            if (r < m.Length - 1 && cell <= m[r + 1][c])
            {
                blobSize += 1 + FindBlobSize(m, r + 1, c);
            }

            // left
            if (c > 1 && cell <= m[r][c - 1])
            {
                blobSize += 1 + FindBlobSize(m, r, c - 1);
            }

            // right
            if (c < m[r].Length - 1 && cell <= m[r][c + 1])
            {
                blobSize += 1 + FindBlobSize(m, r, c + 1);
            }

            return blobSize;
        }
    }
}