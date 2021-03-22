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
            // start
            var b = 106700;
            var c = (b + 17000);
            
            while (true)
            {
                var f = 1;
                var d = 2;
                int g;
                do
                {
                    var e = 2;
                    do
                    {
                        g = d * e - b;
                        if (g == 0)
                        {
                            f = 0;
                            e++;
                        }

                        g = e - b;
                    } while (g != 0);

                    d++;
                    g = d - b;
                } while (g != 0);

                if (f == 0)
                {
                    h++;
                    g = b;
                }

                g -= c;
                if (g == 0)
                {
                    return h.ToString();
                }

                b -= 17;
            }
        }
    }
}