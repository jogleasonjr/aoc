using System;
using System.IO;
using System.Text;

namespace Aoc
{
    public class BingoSquare
    {
        public int Value { get; }
        public bool Marked { get; private set; }

        public BingoSquare(int value)
        {
            Value = value;
        }

        public void Mark()
        {
            Marked = true;
        }

        public override string ToString()
        {
            if (Marked)
            {
                return $"[{Value.ToString().PadLeft(2)}]";
            }
            else
                return $" {Value.ToString().PadLeft(2)} ";
        }
    }

    public class BingoBoard
    {
        public BingoSquare[][] Data { get; }
        public int Stride => Data.GetLength(0);

        public BingoBoard(BingoSquare[][] data)
        {
            Data = data;
        }

        public bool IsComplete()
        {
            // any marked rows
            if (Data.Any(r => r.All(c => c.Marked)))
            {
                return true;
            }

            // any marked columns
            for (int i = 0; i < Stride; i++)
            {
                if (Data.All(r => r[i].Marked))
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var row in Data)
            {
                sb.AppendLine(string.Join(' ', row.Select(s => s.ToString())));
            }

            return sb.ToString();
        }
    }

    public class BingoGame
    {
        private const int Stride = 5;
        public List<BingoBoard> Boards { get; }
        public int[] Numbers { get; }

        private BingoGame(List<BingoBoard> boards, int[] numbers)
        {
            Boards = boards;
            Numbers = numbers;
        }

        public static BingoGame ParseFile(string filename)
        {
            var lines = Util.GetStringsFromFile(filename).Where(line => !string.IsNullOrEmpty(line)).ToArray();
            var numbers = Util.ParseNumbersFromLine(lines[0], ',');
            var boards = new List<BingoBoard>();

            for (int i = 1; i < lines.Length; i += Stride)
            {
                var rows = new BingoSquare[Stride][];

                for (int x = 0; x < Stride; x++)
                {
                    rows[x] = Util.ParseNumbersFromLine(lines[i + x], ' ').Select(n => new BingoSquare(n)).ToArray();
                }

                boards.Add(new BingoBoard(rows));
            }

            return new BingoGame(boards, numbers);
        }

        public int FindLastWinner()
        {
            var boards = Boards.ToList();

            for (int i = 0; i < Numbers.Length; i++)
            {
                var n = Numbers[i];

                foreach (var board in Boards)
                {
                    foreach (var square in board.Data.SelectMany(r => r))
                    {
                        if (square.Value == n)
                        {
                            square.Mark();
                        }
                    }

                    if (board.IsComplete() && boards.Contains(board))
                    {
                        boards.Remove(board);

                        if (boards.Count() == 0)
                        {
                            var unmarkedSum = board.Data.SelectMany(r => r).Where(x => !x.Marked).Select(x => x.Value).Sum();
                            return unmarkedSum * n;
                        }
                    }
                }
            }

            throw new ApplicationException("Couldn't determine loser, n00b");
        }

        public int FindWinner()
        {

            for (int i = 0; i < Numbers.Length; i++)
            {
                var n = Numbers[i];

                foreach (var board in Boards)
                {
                    foreach (var square in board.Data.SelectMany(r => r))
                    {
                        if (square.Value == n)
                        {
                            square.Mark();
                        }
                    }

                    if (board.IsComplete())
                    {
                        // The score of the winning board can now be calculated.
                        // Start by finding the sum of all unmarked numbers on that board; in this case, the sum is 188.
                        // Then, multiply that sum by the number that was just called when the board won, 24, to get the final score,
                        // 188 * 24 = 4512.

                        var unmarkedSum = board.Data.SelectMany(r => r).Where(x => !x.Marked).Select(x => x.Value).Sum();
                        return unmarkedSum * n;
                    }
                }
            }

            throw new ApplicationException("Couldn't determine winner, n00b");
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Join(',', Numbers));

            foreach (var board in Boards)
            {
                sb.AppendLine();

                for (int i = 0; i < board.Stride; i++)
                {
                    sb.AppendLine(string.Join(' ', board.Data[i].Select(n => n.Value.ToString().PadLeft(2))));
                }
            }

            return sb.ToString();
        }
    }
}