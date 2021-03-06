namespace AdventOfCode2017.days
{
    internal class Day09 : Day
    {
        public Day09(string input, bool part1) : base(input, part1)
        {
        }

        protected override string Solve1()
        {
            var deepness = 0;
            var score = 0;
            var garbage = false;
            
            for (var i = 0; i < Input.Length; i++)
            {
                var c = Input[i];
                switch (c)
                {
                    case '<':
                        garbage = true;
                        break;
                    case '>':
                        if (garbage) garbage = false;
                        break;
                    case '{':
                        if(garbage) continue;
                        deepness++;
                        score += deepness;
                        break;
                    case '}':
                        if(garbage) continue;
                        deepness--;
                        break;
                    case '!':
                        i++;
                        break;
                }
            }

            return score.ToString();
        }

        protected override string Solve2()
        {
            var garbageCharacter = 0;
            var garbage = false;
            
            for (var i = 0; i < Input.Length; i++)
            {
                var c = Input[i];
                switch (c)
                {
                    case '<':
                        if (garbage)
                        {
                            garbageCharacter++;
                        }
                        garbage = true;
                        break;
                    case '>':
                        if (garbage) garbage = false;
                        break;
                    case '!':
                        i++;
                        break;
                    default:
                        if (garbage)
                        {
                            garbageCharacter++;
                        }
                        break;
                }
            }

            return garbageCharacter.ToString();
        }
    }
}