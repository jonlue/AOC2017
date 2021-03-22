using System;
using System.Collections.Generic;

namespace AdventOfCode2017.days
{
    internal class Day24 : Day
    {
        private List<(int, int)> Components { get; set; }
        public bool Part2 { get; set; }

        public Day24(string input, bool part1) : base(input, part1)
        {
            CreateComponents();
        }

        protected override string Solve1()
        {
            return FindStrongestBridge().ToString();
        }

        protected override string Solve2()
        {
            return FindStrongestBridge(true).ToString();
        }

        private int FindStrongestBridge(bool part2 = false)
        {
            var copy = new List<(int, int)>();
            copy.AddRange(Components);
            return FindStrongestBridge(0, 0, copy, 0, part2).Item1;
        }

        private (int, int) FindStrongestBridge(int sum, int port, List<(int, int)> components, int size, bool part2)
        {
            var max = sum;
            var maxSize = size;
            foreach (var (inPort, outPort) in components)
            {
                if (inPort == port || outPort == port)
                {
                    var copy = new List<(int, int)>();
                    copy.AddRange(components);
                    copy.Remove((inPort, outPort));
                    var (newMax, newMaxSize) = FindStrongestBridge(sum + inPort + outPort
                        , inPort == port ? outPort : inPort, copy, size + 1, part2);
                    if (part2)
                    {
                        if (newMaxSize > maxSize)
                        {
                            maxSize = newMaxSize;
                            max = newMax;
                            continue;
                        }
                        if (newMaxSize == maxSize) max = Math.Max(max, newMax);
                    }
                    else
                    {
                        Math.Max(max, newMaxSize);
                    }
                }
            }

            return (max, maxSize);
        }


        private void CreateComponents()
        {
            Components = new List<(int, int)>();
            foreach (var component in Input.Split("\n"))
            {
                var values = Array.ConvertAll(component.Split("/"), int.Parse);
                Components.Add((values[0], values[1]));
            }
        }
    }
}