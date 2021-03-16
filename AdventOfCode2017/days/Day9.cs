using System;
using System.IO;

namespace AdventOfCode2017.days
{
    internal class Day9 : Day
    {
        public Day9(bool part1) : base(part1)
        {
            Input = File.ReadAllText(
                "C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input9.txt");
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