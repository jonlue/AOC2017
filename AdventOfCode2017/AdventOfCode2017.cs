using System;
using System.IO;
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
        
        private static void DoExercise(int exercise, int part)
        {
            Day day = GetDay(ReadFile(exercise), exercise, part == 1);
            Console.WriteLine(day.Solve());
        }

        private static Day GetDay(string input, int exercise, bool part)
        {
            Day day = exercise switch
            {
                (1)  => new Day01(input, part),
                (2)  => new Day02(input, part),
                (3)  => new Day03(input, part),
                (4)  => new Day04(input, part),
                (5)  => new Day05(input, part),
                (6)  => new Day06(input, part),
                (7)  => new Day07(input, part),
                (8)  => new Day08(input, part),
                (9)  => new Day09(input, part),
                (10) => new Day10(input, part),
                (11) => new Day11(input, part),
                (12) => new Day12(input, part),
                (13) => new Day13(input, part),
                (14) => new Day14(input, part),
                (15) => new Day15(input, part),
                (16) => new Day16(input, part),
                (17) => new Day17(input, part),
                (18) => new Day18(input, part),
                (19) => new Day19(input, part),
                (20) => new Day20(input, part),
                (21) => new Day21(input, part),
                (22) => new Day22(input, part),
                (23) => new Day23(input, part),
                (24) => new Day24(input, part),/*
                (25) => new Day25(input, part),
                */
                _ => null
            };
            return day;
        }
        
        private static string ReadFile(int exercise)
        {
            var fileName = "input";
            if (exercise < 10) fileName += "0" ;
            fileName += (exercise + ".txt");
            
            var path = $"{Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"))}resources\\" + fileName;
            
            return File.ReadAllText(path)
                .Replace("\r", "");
        }
    }
    
}
