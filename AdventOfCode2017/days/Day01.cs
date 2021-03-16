using System.IO;
using System.Linq;

namespace AdventOfCode2017.days
{
    internal class Day01 : Day
    {
        public Day01(bool part1) : base(part1)
        {
            Input = File.ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input01.txt");
        }
    

        protected override string Solve1()
        {
            var sum = 0;
            for (var i = 0; i < Input.Length-1; i++)
            {
                if (Input[i] == Input[i + 1])
                {
                    sum += int.Parse(Input[i].ToString());
                }
            }
            
            sum += Input.First() == Input.Last() ? int.Parse(Input.First().ToString()) : 0;

            return sum.ToString();
        }

        protected override string Solve2()
        {
            var step = Input.Length/2;
            var sum = 0;
            for (var i = 0; i < Input.Length-1; i++)
            {
                if (Input[i] == Input[(i + step) % Input.Length])
                {
                    sum += int.Parse(Input[i].ToString());
                }
            }
            
            sum += Input.First() == Input[step] ? int.Parse(Input.First().ToString()) : 0;

            return sum.ToString();
        }
    }
}