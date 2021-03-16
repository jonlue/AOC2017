using System;
using System.IO;

namespace AdventOfCode2017.days
{
    internal class Day05 : Day
    {
        private int[] JumpInstructions { get; set; }
        public Day05(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input05.txt")
                .Replace("\r","");
            JumpInstructions = Array.ConvertAll(Input.Split("\n"), int.Parse);
        }

        protected override string Solve1()
        {
            var steps = 0;
            try
            {
                var index = 0;
                while (true)
                {
                    var newIndex = JumpInstructions[index];
                    JumpInstructions[index]++;
                    index += newIndex;
                    steps++;   
                }
            }
            catch (IndexOutOfRangeException)
            {
                // Do Nothing, expected Result
            }

            return steps.ToString();
        }

        protected override string Solve2()
        {
            var steps = 0;
            try
            {
                var index = 0;
                while (true)
                {
                    var newIndex = JumpInstructions[index];
                    JumpInstructions[index] += JumpInstructions[index] >= 3 ? -1 : 1;
                    index += newIndex;
                    steps++;   
                }
            }
            catch (IndexOutOfRangeException)
            {
                // Do Nothing, expected Result
            }

            return steps.ToString();
        }
    }
}