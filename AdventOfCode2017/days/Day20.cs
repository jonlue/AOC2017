using System;
using System.Collections.Generic;
using System.Numerics;
using AdventOfCode2017.util;

namespace AdventOfCode2017.days
{
    internal class Day20 : Day
    {
        private List<Particle> Particles { get; set; }
        public Day20(string input, bool part1) : base(input, part1)
        {
            Input = Input.Replace("<", "")
                .Replace(" ", "")
                .Replace("p", "")
                .Replace("v", "")
                .Replace("a", "")
                .Replace("=","")
                .Replace(">,",">");
            GenerateParticles();
        }

        protected override string Solve1()
        {
            var index = -1;
            var i = 0;
            var min = int.MaxValue;
            
            foreach (var particle in Particles)
            {
                particle.FakeSteps(100000);
                var distance = particle.GetManhattanDistance();
                if (distance < min)
                {
                    min = distance;
                    index = i;
                }

                i++;
            }

            return index.ToString();
        }

        protected override string Solve2()
        {
            for (var i = 0; i < 10000; i++)
            {
                var positions = new Dictionary<Vector3, List<int>>(1000);
                var j = 0;
                foreach (var particle in Particles)
                {
                    particle.MakeStep();

                    if (!positions.TryAdd(particle.Position, new List<int>() {j}))
                    {
                        positions[particle.Position].Add(j);
                    }

                    j++;
                }

                var count = 0;
                foreach (var indices in positions.Values)
                {
                    if (indices.Count < 2) continue;
                    foreach (var index in indices)
                    {
                        Particles.RemoveAt(index - count);
                        count++;
                    }
                }
            }

            return Particles.Count.ToString();
        }

        private void GenerateParticles()
        {
            Particles = new List<Particle>();
            foreach (var particle in Input.Split("\n"))
            {
                var p = particle.Split(">");
                Particles.Add(new Particle(Array.ConvertAll(p[0].Split(","), int.Parse)
                    ,Array.ConvertAll(p[1].Split(","), int.Parse)
                    ,Array.ConvertAll(p[2].Split(","), int.Parse)));
            }
        }
        
    }
}