using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.days
{
    internal class Day08 : Day
    {
        private string[] Instructions { get; }
        private Dictionary<string, int> Registers { get; }
        private int MaxValueInRegister { get; set; }

        public Day08(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input08.txt")
                .Replace("\r", "");
            Instructions = Input.Split("\n");
            Registers = new Dictionary<string, int>();
            MaxValueInRegister = int.MinValue;
        }

        protected override string Solve1()
        {
            RunInstructions();
            return Registers.Values
                .Prepend(int.MinValue)
                .Max()
                .ToString();
        }

        private void RunInstructions()
        {
            foreach (var instruction in Instructions)
            {
                var parts = instruction.Split(" ");
                var register = parts[0];
                var add = parts[1].Equals("inc");
                var value = int.Parse(parts[2]);
                var condRegister = parts[4];
                var condition = parts[5];

                var condValue = int.Parse(parts[6]);

                //Init Registers if missing
                if (!Registers.ContainsKey(register))
                {
                    Registers.Add(register, 0);
                }

                if (!Registers.ContainsKey(condRegister))
                {
                    Registers.Add(condRegister, 0);
                }

                //check condition
                var conditionMet = condition switch
                {
                    (">") => Registers[condRegister] > condValue,
                    (">=") => Registers[condRegister] >= condValue,
                    ("<") => Registers[condRegister] < condValue,
                    ("<=") => Registers[condRegister] <= condValue,
                    ("==") => Registers[condRegister] == condValue,
                    ("!=") => Registers[condRegister] != condValue,
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (!conditionMet) continue;
                if (add)
                {
                    Registers[register] += value;
                }
                else
                {
                    Registers[register] -= value;
                }

                MaxValueInRegister = Math.Max(MaxValueInRegister, Registers[register]);
            }
        }

        protected override string Solve2()
        {
            RunInstructions();
            return MaxValueInRegister.ToString();
        }
    }
}