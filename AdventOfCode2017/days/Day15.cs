using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace AdventOfCode2017.days
{

    internal class Day15 : Day
    {
        private const int GeneratorAFactor = 16807;
        private const int GeneratorBFactor = 48271;
        private const int Mod = 2147483647;
        private const long Mask = (1 << 16) - 1;
        private long GeneratorA { get; set; }
        private long GeneratorB { get; set; }
        public Day15(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input15.txt")
                .Replace("\r", "");
            var generators = Input.Split("\n").Select(line => Convert.ToInt64(line.Split(" ").Last())).ToArray();
            GeneratorA = generators[0];
            GeneratorB = generators[1];
        }

        protected override string Solve1()
        {
            var pairs = 0;
            
            for (var j = 0; j < 40000000; j++)
            {
                //generate value;
                GeneratorA = (GeneratorA * GeneratorAFactor) % Mod; 
                GeneratorB = (GeneratorB * GeneratorBFactor) % Mod;

                //compare lowest 16 bit
                if ((GeneratorA & Mask) == (GeneratorB & Mask)) pairs++;
            }
            return pairs.ToString();
        }

        protected override string Solve2()
        {
            var pairs = 0;
            var queueA = new Queue<long>();//new ConcurrentQueue<long>();
            var queueB = new Queue<long>();//new ConcurrentQueue<long>();
            var genA = new Thread(() => GeneratorThread(GeneratorA, GeneratorAFactor, queueA, 4));
            var genB = new Thread(() => GeneratorThread(GeneratorB, GeneratorBFactor, queueB, 8));
            
            genA.Start();
            genB.Start();
            
            /*
            //find  better dequeue?
            do
            {
                while(!queueA.IsEmpty && !queueB.IsEmpty)
                {
                    long a;
                    long b;
                    queueA.TryDequeue(out a);
                    queueB.TryDequeue(out b);
                    pairs += (a & Mask) == (b & Mask) ? 1 : 0;
                };
            } while (genA.IsAlive || genB.IsAlive);
            */
            
            genA.Join();
            genB.Join();

            while(queueA.Count > 0)
            {
                pairs += (queueA.Dequeue() & Mask) == (queueB.Dequeue() & Mask) ? 1 : 0;
            }
            
            return pairs.ToString();
        }

        private static void GeneratorThread(long value, long factor, Queue<long> q, int limit)
        {
            for (var i = 0; i < 5000000; i++)
            {
                do
                {
                    value = (value * factor) % Mod;
                } while (value % limit != 0);
                q.Enqueue(value);
            }
        }
        
    }
    
}