using System;
using System.Linq;

namespace AdventOfCode2017.days
{
    internal class Day25 : Day
    {
        private int[] Tape { get; set; }
        private const int Size = 10000;
        private int Position { get; set; }
        private State MachineState { get; set; }
        private long Step { get; set; }
        public Day25(string input, bool part1) : base(input, part1)
        {
            Tape = new int[Size];
            Position = Size / 2;
            MachineState = State.A;
            GetStep();
        }

        protected override string Solve1()
        {
            Console.WriteLine("Result may not be based on Input");
            for (var i = 0; i < Step; i++)
            {
                var isZero = Tape[Position] == 0;
                switch (MachineState)
                {
                    case State.A:
                        Tape[Position] = isZero ? 1 : 0;
                        MachineState = isZero ? State.B : State.C;
                        Position++;
                        break;
                    case State.B:
                        Tape[Position] = 0;
                        MachineState = isZero ? State.A : State.D;
                        Position += isZero ? -1 : 1;
                        break;
                    case State.C:
                        Tape[Position] = 1;
                        MachineState = isZero ? State.D : State.A;
                        Position++;
                        break;
                    case State.D:
                        Tape[Position] = isZero ? 1 : 0;
                        MachineState = isZero ? State.E : State.D;
                        Position--;
                        break;
                    case State.E:
                        Tape[Position] = 1;
                        MachineState = isZero ? State.F : State.B;
                        Position += isZero ? 1 : -1;
                        break;
                    case State.F:
                        Tape[Position] = 1;
                        MachineState = isZero ? State.A : State.E;
                        Position++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return Tape.Sum().ToString();
        }

        protected override string Solve2()
        {
            return "You did it! \nCongratulations!";
        }
        
        private void GetStep()
        {
            Step = long.Parse(
                Input.Split("\n")[1]
                    .Split(" ")[^2]);
        }

        private enum State
        {
            A,B,C,D,E,F
        }
    }
}