using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.days
{
    internal class Day12 : Day
    {
        private HashSet<int>  UniquePrograms { get; set; }
        private Dictionary<int, int[]> Connections { get; set; }
        public Day12(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input12.txt")
                .Replace("\r", "");
            UniquePrograms = new HashSet<int>();
            Connections = Input.Split("\n")
                .Select(line => Array.ConvertAll(line.Replace(" ", "")
                    .Replace("<->", ",")
                    .Split(","), int.Parse))
                .ToDictionary(ints => ints[0], ints => ints.Skip(1).ToArray());
        }

        protected override string Solve1()
        {
            UniquePrograms.Add(0);
            AddPrograms(0);

            return UniquePrograms.Count.ToString();
        }

        private void AddPrograms(int prog)
        {
            foreach (var sub in Connections[prog])
            {
                if (UniquePrograms.Contains(sub))
                {
                    continue;
                }

                UniquePrograms.Add(sub);
                AddPrograms(sub);
            }
        }

        protected override string Solve2()
        {
            var numGroups = 0;
            while (Connections.Count != 0)
            {
                var start = Connections.Keys.First();
                UniquePrograms.Add(start);
                AddPrograms(start);

                foreach (var program in UniquePrograms)
                {
                    Connections.Remove(program);
                }

                numGroups++;
            }

            return numGroups.ToString();
        }
    }
}