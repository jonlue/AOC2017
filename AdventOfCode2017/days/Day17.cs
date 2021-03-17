using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2017.days
{
    internal class Day17 : Day
    {
        private int Value { get; }
        public Day17(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input17.txt");
            Value = int.Parse(Input);
        }

        protected override string Solve1()
        {
            var a = new List<int>() {0,1};
            var index = 1;

            for (var i = 2; i < 2018; i++)
            {
                for (var j = 0; j < Value; j++)
                {
                    index = (index + 1) % a.Count;
                }

                a.Insert(index + 1, i);
                index++;
            }

            return a[(index + 1) % a.Count].ToString();
        }

        protected override string Solve2()
        {
            var index = 0;
            var offset = 0;
            var res = -1;
            for (var i = 1; i < 50_000_001; i++)
            {
                index = (index + Value) % i + 1;
                if (index == 1 + offset)
                {
                    res = i;
                }

                if (index == 0)
                {
                    offset++;
                }
            }

            return res.ToString();
        }
    }
}