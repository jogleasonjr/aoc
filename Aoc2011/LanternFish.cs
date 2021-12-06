using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Aoc
{
    public class LanternFish
    {
        public int CycleCount { get; private set; }
        public int DaysLeft { get; private set; }

        public LanternFish(int daysLeft)
        {
            DaysLeft = daysLeft;
            CycleCount = 1;
        }

        public bool Cycle()
        {
            DaysLeft--;

            if (DaysLeft == -1)
            {
                CycleCount++;
                DaysLeft = 6;
                return true;
            }

            return false;
        }
    }

    public class LanternFishSchool
    {
        public ConcurrentBag<LanternFish> Bag { get; }

        public List<LanternFish> Ocean { get; }

        public LanternFishSchool(int[] daysLeft)
        {
            var fish = daysLeft.ToList().Select(f => new LanternFish(f)).ToList();
            Bag = new ConcurrentBag<LanternFish>(fish);

            Ocean = new List<LanternFish>();
        }

        public void Cycle(int days, bool verbose = false)
        {
            for (int i = 0; i < days; i++)
            {
                var sw = Stopwatch.StartNew();
                var newFish = new ConcurrentBag<LanternFish>();
                Parallel.ForEach(Bag, fish =>
                {
                    var spawned = fish.Cycle();
                    if (spawned)
                    {
                        newFish.Add(new LanternFish(8));
                    }
                });

                foreach (var fish in newFish) Bag.Add(fish);

                if (verbose)
                    Console.WriteLine($"Day {i} of {days} | {Bag.Count} fish | {sw.Elapsed} elapsed");

                sw.Restart();
            }
        }

        public void Print(int days)
        {
            Console.WriteLine("Initial state: " + Stringify());
            for (int i = 0; i < days; i++)
            {
                Cycle(1);
                Console.WriteLine($"After {(i + 1).ToString().PadLeft(2)} days: {Stringify()}");
            }
        }

        public string Stringify()
        {
            return string.Join(",", Bag.Select(f => f.DaysLeft));
        }
    }
}