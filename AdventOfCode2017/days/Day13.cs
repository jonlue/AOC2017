using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2017.util;

namespace AdventOfCode2017.days
{
    internal class Day13 : Day
    {
        private List<Firewall> Firewalls { get; set; }
        public Day13(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input13.txt");
            Firewalls = new List<Firewall>();
            CreateFirewalls();
        }

        protected override string Solve1()
        {
            return GoThroughFirewalls().ToString();
        }
        
        protected override string Solve2()
        {

            var mod = Input.Split("\n")
                .Select(line => Array.ConvertAll(line.Split(":"), int.Parse))
                .Select(vs => (vs[0], vs[1]+vs[1]-2)).ToList();

            //value += 12 because first 3 mod
            var value = -6;
            bool found;
            do
            {
                value += 12;
                found = mod.All(tuple => (value + tuple.Item1) % tuple.Item2 != 0);
            } while (!found);
            
            return value.ToString();
            
        }

        private int GoThroughFirewalls()
        {
            var severity = 0;
            foreach (var t in Firewalls)
            {
                severity += t.Caught();
                MoveScanner(1);
            }

            return severity;
        }
        
        private void CreateFirewalls()
        {
            var count = 0;
            foreach (var line in Input.Split("\n"))
            {
                var values = Array.ConvertAll(line.Split(":"), int.Parse);
                while (count != values[0])
                {
                    Firewalls.Add(new Firewall(count,0));
                    count++;
                }
                Firewalls.Add(new Firewall(values[0],values[1]));
                count++;
            }
        }

        private void MoveScanner(int times)
        {
            foreach (var firewall in Firewalls)
            {
                for (var i = 0; i < times; i++)
                {
                    firewall.Step();
                }
            }
        }
        
    }
}