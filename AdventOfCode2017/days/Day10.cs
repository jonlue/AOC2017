using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.days
{
    internal class Day10 : Day
    {
        private List<int> Knot { get; set; }
        private const string EndLengths = "17,31,73,47,23";

        public Day10(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input10.txt")
                .Replace("\r","");
            Knot = new List<int>();
            Knot.AddRange(Enumerable.Range(0, 256));
        }

        protected override string Solve1()
        {
            RunHash(Input);
            return (Knot[0] * Knot[1]).ToString();
        }

        protected override string Solve2()
        {
            // generate new lengths
            var convertedList = Input.Aggregate("", (current, c) => current + ((int) c + ","));
            convertedList += EndLengths;

            // run Hash 64 times
            var pos = (0, 0);
            for (var i = 0; i < 64; i++)
            {
                pos = RunHash(convertedList,pos);
            }

            // generate deep hash
            // XOR each 16 parts
            var deepHash = "";
            for (var i = 0; i < (256 / 16); i++)
            {
                var value = Knot[i*16];
                for (var j = (i * 16) + 1; j < (i * 16 + 16); j++)
                {
                    value ^= Knot[j];
                }

                deepHash += value.ToString("X").ToLower();
            }

            return deepHash;
        }

        private void RunHash(string lengths)
        {
            RunHash(lengths, (0, 0));
        }
        private (int,int) RunHash(string lengths, (int,int) pos)
        {
            var (item1, item2) = pos;
            var position = item1;
            var skipSize = item2;

            foreach (var length in Array.ConvertAll(lengths.Split(","),int.Parse))
            {
                if(length > Knot.Count) continue;
                //Reverse
                if (length != 1)
                {
                    // Snap around list  
                    if (position + length > Knot.Count)
                    {
                        var lengthEnd = Knot.Count - position;

                        var temp1 = Knot.GetRange(position, lengthEnd);
                        temp1.AddRange(Knot.GetRange(0, length - lengthEnd));
                        temp1.Reverse();

                        Knot.RemoveRange(position, lengthEnd);
                        Knot.InsertRange(position, temp1.GetRange(0, lengthEnd));

                        temp1.RemoveRange(0, lengthEnd);

                        Knot.RemoveRange(0, length - lengthEnd);
                        Knot.InsertRange(0, temp1);
                    }
                    else
                    {
                        var temp = Knot.GetRange(position, length);
                        temp.Reverse();

                        Knot.RemoveRange(position, length);
                        Knot.InsertRange(position, temp);
                    }
                }

                //Move
                position = (position + length + skipSize) % Knot.Count;

                //Increase
                skipSize++;
            }
            return (position,skipSize);
        }
    }
}