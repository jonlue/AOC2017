using AdventOfCode2017.util;

namespace AdventOfCode2017.days
{
    internal class Day10 : Day
    {
        private KnotHash KnotHash { get; set; }

        public Day10(string input, bool part1) : base(input, part1)
        {
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