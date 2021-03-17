using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace AdventOfCode2017.util
{
    public class TabletAssembly
    {
        private const string Send = "snd";
        private const string Set = "set";
        private const string Add = "add";
        private const string Multiply = "mul";
        private const string Modulo = "mod";
        private const string Recover = "rcv";
        private const string JumpGreaterZero = "jgz";
        private Dictionary<string,long> Registers { get; }
        private string[] Instructions { get; }
        private ConcurrentQueue<long> QOut { get; }
        private ConcurrentQueue<long> QIn { get; }
        private bool Part1 { get; init; }
        public int MessagesSent { get; private set; }
        public bool WaitingForMessage { get; private set; }
        public bool ShutDown { get; set; }

        public TabletAssembly(string[] instructions, ConcurrentQueue<long> qOut, ConcurrentQueue<long> qIn, bool part1 = false)
        {
            Instructions = instructions;
            MessagesSent = 0;
            WaitingForMessage = false;
            QOut = qOut;
            QIn = qIn;
            Part1 = part1;
            Registers = new Dictionary<string, long>();
        }

        public void Run()
        {
            for(var i = 0; i< Instructions.Length; i++)
            {
                var parameters = Instructions[i].Split(" ");
                var mode = parameters[0];
                long value;
                switch (mode)
                {
                    case Send:
                        value = GetValue(parameters[1]);
                        QOut.Enqueue(value);
                        MessagesSent++;
                        break;
                    case Recover:
                        if (Part1)
                        {
                            if (GetValue(parameters[1]) != 0) return;
                        }
                        else
                        {
                            while (!QIn.TryDequeue(out value))
                            {
                                WaitingForMessage = true;
                                Thread.Sleep(10);
                                if(ShutDown) return;
                            }
                            WaitingForMessage = false;
                            Registers[parameters[1]] = value;
                        }
                        break;
                    case JumpGreaterZero:
                        var num = GetValue(parameters[1]);
                        value = GetValue(parameters[2]);
                        if (num > 0) i += (int) value - 1;
                        break;
                    default:
                        var reg = parameters[1];
                        if (!Registers.ContainsKey(reg)) Registers.Add(reg,0);
                        value = GetValue(parameters[2]);
                        
                        Registers[reg] = mode switch
                        {
                            (Set) => value,
                            (Add) => Registers[reg] + value,
                            (Multiply) => Registers[reg] * value,
                            (Modulo) => value != 0 ? Registers[reg] % value : Registers[reg],
                            _ => Registers[reg]
                        };
                        break;
                }
                
            }

            Console.Out.WriteLine("stop");
            ShutDown = true;
        }

        public void SetRegister(string reg, int value)
        {
            Registers.Add(reg,value);
        }
        
        private long GetValue(string parameter)
        {
            try
            {
                return int.Parse(parameter);
            }
            catch (FormatException)
            {
                if(!Registers.ContainsKey(parameter)) Registers.Add(parameter,0);
                return Registers[parameter];
            }
        }
        
    }
}