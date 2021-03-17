namespace AdventOfCode2017
{
    internal abstract class Day
    {
        private bool Part1 { get; set; }
        protected string Input;

        protected Day(string input, bool part1)
        {
            Input = input;
            Part1 = part1;
        }

        public string Solve()
        {
            return Part1 ? Solve1() : Solve2();
        }

        protected abstract string Solve1();
        protected abstract string Solve2();

    }
}