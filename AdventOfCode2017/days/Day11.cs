using System;

namespace AdventOfCode2017.days
{
    internal class Day11 : Day
    {
        private const string Nw = "nw";
        private const string Ne = "ne";
        private const string Sw = "sw";
        private const string Se = "se";
        private const string No = "n";
        private const string So = "s";
        public Day11(string input, bool part1) : base(input, part1)
        {
        }

        protected override string Solve1()
        {
            var x = 0;
            var y = 0;
            foreach (var direction in Input.Split(","))
            {
                var (newX, newY) = GetStep(direction);
                x += newX;
                y += newY;
            }
            
            return GetDistance(x, y).ToString();
        }

        private int GetDistance(int x, int y)
        {
            var distance = Math.Abs(0 - x);
            y = Math.Abs(y) - distance;
            return distance + Math.Abs(0 - y)/2;
        }

        private (int, int) GetStep(string dir)
        {
            return dir switch
            {
                (Nw) => (-1, -1),
                (Ne) => (1, -1),
                (Sw) => (-1, 1),
                (Se) => (1, 1),
                (No) => (0, -2),
                (So) => (0, 2),
                _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
            };
        }

        protected override string Solve2()
        {
            var max = int.MinValue;
            
            var x = 0;
            var y = 0;
            foreach (var direction in Input.Split(","))
            {
                var (newX, newY) = GetStep(direction);
                x += newX;
                y += newY;

                max = Math.Max(max, GetDistance(x, y));
            }

            return max.ToString();
        }
    }
}