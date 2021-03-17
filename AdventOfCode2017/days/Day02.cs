using System;

namespace AdventOfCode2017.days
{
    internal class Day02 : Day
    {
        public Day02(string input, bool part1) : base(input,part1)
        {
        }

        protected override string Solve1()
        {
            var checksumSum = 0;
            foreach (var line in Input.Split("\n"))
            {
                var min = int.MaxValue;
                var max = int.MinValue;
                foreach (var num in line.Split("\t"))
                {
                    var n = int.Parse(num);
                    min = Math.Min(min, n);
                    max = Math.Max(max, n);
                }

                checksumSum += (max - min);
            }

            return checksumSum.ToString();
        }

        protected override string Solve2()
        {
            var sumDivision = 0;
            foreach (var line in Input.Split("\n"))
            {
                foreach (var num1 in line.Split("\t"))
                {
                    var n1 = int.Parse(num1);
                    foreach (var num2 in line.Split("\t"))
                    {
                        var n2 = int.Parse(num2);
                        if (n1 == n2 || n1 % n2 != 0) continue;
                        sumDivision += (Math.Max(n1 / n2, n2 / n1));
                        break;
                    }
                }
            }

            return sumDivision.ToString();
        }
    }
}