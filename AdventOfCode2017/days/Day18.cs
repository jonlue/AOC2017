using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using AdventOfCode2017.util;

namespace AdventOfCode2017.days
{
    internal class Day18 : Day
    {

        public Day18(bool part1) : base(part1)
        {
            Input = File.ReadAllText(
                "C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input18.txt")
                .Replace("\r","");
        }

        protected override string Solve1()
        {
            var qOut = new ConcurrentQueue<long>();
            var qIn = new ConcurrentQueue<long>();

            var assembly = new TabletAssembly(Input.Split("\n"), qOut, qIn, true);
            var tabletAssembly = new Thread(() => assembly.Run());
            
            tabletAssembly.Start();

            tabletAssembly.Join();
            return qOut.ToArray().Last().ToString();
        }



        protected override string Solve2()
        {
            var qOut = new ConcurrentQueue<long>();
            var qIn = new ConcurrentQueue<long>();

            var assembly0 = new TabletAssembly(Input.Split("\n"), qOut, qIn);
            var assembly1 = new TabletAssembly(Input.Split("\n"), qIn, qOut );
            assembly0.SetRegister("p",0);
            assembly1.SetRegister("p",1);
            var tabletAssembly0 = new Thread(() => assembly0.Run());
            var tabletAssembly1 = new Thread(() => assembly1.Run());
            
            tabletAssembly0.Start();
            tabletAssembly1.Start();

            
            while ((!assembly0.WaitingForMessage && !assembly1.WaitingForMessage) || !qIn.IsEmpty || !qOut.IsEmpty)
            {
                Thread.Sleep(10);
            }
            
            assembly0.ShutDown = true;
            assembly1.ShutDown = true;

            return assembly1.MessagesSent.ToString();
        }
    }
}