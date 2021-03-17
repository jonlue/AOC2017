using System.IO;
using AdventOfCode2017.util;

namespace AdventOfCode2017.days
{
    internal class Day10 : Day
    {
        private KnotHash KnotHash { get; set; }

        public Day10(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input10.txt")
                .Replace("\r", "");

        }

        protected override string Solve1()
        {
            KnotHash = new KnotHash(Input, false);
            KnotHash.RunHash(1);
            return (KnotHash.Knots[0] * KnotHash.Knots[1]).ToString();
        }

        protected override string Solve2()
        {
            KnotHash = new KnotHash(Input);
            KnotHash.RunHash();
            return KnotHash.Hash;
        }

    }
}