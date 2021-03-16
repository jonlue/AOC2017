using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.days
{
    internal class Day06 : Day
    {
        private int[] MemoryBank { get; set; }
        public Day06(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input06.txt");
            MemoryBank = Array.ConvertAll(Input.Split("\t"), int.Parse);
        }

        protected override string Solve1()
        {
            var memoryConfiguration = new HashSet<string>();

            do
            {
                memoryConfiguration.Add(ArrayToString(MemoryBank));
                MemoryBank = Rearrange(MemoryBank);
            } while (!memoryConfiguration.Contains(ArrayToString(MemoryBank)));

            return memoryConfiguration.Count.ToString();
        }

        private int[] Rearrange(int[] arr)
        {
            var indexMax = IndexOfMax(arr);
            var index = indexMax.Item1;
            var max = indexMax.Item2;
            arr[index] = 0;
            for (var i = 0; i < max; i++)
            {
                index = (index + 1) % arr.Length;
                arr[index]++;
            }

            return arr;
        }

        private (int,int) IndexOfMax(int[] arr)
        {
            var index = 0;
            var max = -1;
            for (var i = 0; i < arr.Length; i++)
            {
                if (max >= arr[i]) continue;
                max = arr[i];
                index = i;
            }

            return (index, max);
        }

        private string ArrayToString(IEnumerable<int> arr)
        {
            return arr.Aggregate("", (current, v) => current + "," + v).Substring(1);
        }

        protected override string Solve2()
        {
            var memoryConfiguration = new Dictionary<string, int>();

            var count = 0;
            do
            {
                memoryConfiguration.Add(ArrayToString(MemoryBank), count);
                MemoryBank = Rearrange(MemoryBank);
                count++;
            } while (!memoryConfiguration.ContainsKey(ArrayToString(MemoryBank)));

            return (count - memoryConfiguration[ArrayToString(MemoryBank)]).ToString();
        }
    }
}