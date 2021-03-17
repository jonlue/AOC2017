using System;
using AdventOfCode2017.days;

namespace AdventOfCode2017
{
    internal static class Aoc17
    {
        private static void Main(string[] args)
        {
            var exercise = 0;
            var part = 0;
            
            if (args.Length > 2 || args.Length < 1) {
                Usage();
            } else {
                try {
                    exercise = int.Parse(args[0]);
                    part = int.Parse(args[1]);
                    if (exercise > 25 || exercise < 1 || part < 1 || part > 2) {
                        Usage();
                    }
                } catch (Exception e) {
                    Console.Write(e.StackTrace);
                    Usage();
                }
            }
            DoExercise(exercise, part);
        }
        
        private static void Usage() {
            Console.WriteLine("usage: exercise part");
            Console.WriteLine("usage:  [0-25]  [1,2]?");
        }
        
        private static void DoExercise(int exercise, int part) {
            Day day = GetDay(exercise, part == 1);
            Console.WriteLine(day.Solve());
        }
        
        private static Day GetDay(int exercise, bool part)
        {
            Day day = exercise switch
            {
                (1)  => new Day01(part),
                (2)  => new Day02(part),
                (3)  => new Day03(part),
                (4)  => new Day04(part),
                (5)  => new Day05(part),
                (6)  => new Day06(part),
                (7)  => new Day07(part),
                (8)  => new Day08(part),
                (9)  => new Day09(part),
                (10) => new Day10(part),
                (11) => new Day11(part),
                (12) => new Day12(part),
                (13) => new Day13(part),
                (14) => new Day14(part),
                (15) => new Day15(part),
                (16) => new Day16(part),
                (17) => new Day17(part),
                /*
                (18) => new Day18(part),
                (19) => new Day19(part),
                (20) => new Day20(part),
                (21) => new Day21(part),
                (22) => new Day22(part),
                (23) => new Day23(part),
                (24) => new Day24(part),
                (25) => new Day25(part),
                */
                _ => null
            };
            return day;
        }
    }
    
}
