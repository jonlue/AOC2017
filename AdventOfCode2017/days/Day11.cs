using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AdventOfCode2017.days
{
    internal class Day11 : Day
    {
        private const string NW = "nw";
        private const string NE = "ne";
        private const string SW = "sw";
        private const string SE = "se";
        private const string No = "n";
        private const string So = "s";
        public Day11(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input11.txt");
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
                (NW) => (-1, -1),
                (NE) => (1, -1),
                (SW) => (-1, 1),
                (SE) => (1, 1),
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