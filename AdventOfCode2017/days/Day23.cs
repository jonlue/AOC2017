using AdventOfCode2017.util;

namespace AdventOfCode2017.days
{
    internal class Day23 : Day
    {
        private TabletAssembly Assembly { get; set; }
        public Day23(string input, bool part1) : base(input, part1)
        {
            Assembly = new TabletAssembly(Input.Split("\n"),null,null);
        }

        protected override string Solve1()
        {
            Assembly.Run();
            return Assembly.MultCount.ToString();
        }

        protected override string Solve2()
        {
            var h = 0;

            var b = GetB();
            var c = b - GetC();
            var step = -GetStep();
            
            while(b <= c){
                for(var i = 2; i< b; i++)
                {
                    if (b % i != 0) continue;
                    h++;
                    break;
                }
                b += step;
            }

            return h.ToString();
        }

        private int GetStep()
        {
            var ins = Input.Split("\n");
            return int.Parse(ins[^2].Split(" ")[^1]);
        }

        private int GetC()
        {
            var ins = Input.Split("\n");
            return int.Parse(ins[7].Split(" ")[^1]);
        }

        private int GetB()
        {
            var ins = Input.Split("\n");
            var b = int.Parse(ins[0].Split(" ")[^1]);
            b *= int.Parse(ins[4].Split(" ")[^1]);
            b -= int.Parse(ins[5].Split(" ")[^1]);
            return b;
        }
    }
}