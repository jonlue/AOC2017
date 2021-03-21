using System;

namespace AdventOfCode2017.days
{
    internal class Day19 : Day
    {
        private string[] NetworkDiagram { get; set; }
        public Day19(string input, bool part1) : base(input, part1)
        {
            NetworkDiagram = Input.Split("\n");
        }

        protected override string Solve1()
        {
            return TraceNetwork().Item1;
        }
        
        protected override string Solve2()
        {
            return TraceNetwork().Item2.ToString();
        }

        private (string, int) TraceNetwork()
        {
            var running = true;
            var x = FindStart();
            var y = 0;
            var dir = Direction.Down;
            var steps = -1;

            var resultText = "";
            
            while (running)
            {
                var charAtPosition = NetworkDiagram[y][x];
                switch (charAtPosition)
                {
                    case '+':
                        dir = GetNewDirection(dir, x, y);
                        break;
                    case ' ':
                        running = false;
                        break;
                    case '|':
                    case '-':
                        break;
                    default:
                        resultText += charAtPosition;
                        break;
                }
                
                var (newX, newY) = GetNextCoordinate(dir, x, y);
                x = newX;
                y = newY;
                steps++;
            }


            return (resultText, steps);
        }

        private Direction GetNewDirection(Direction dir, int x, int y)
        {
            switch (dir)
            {
                case Direction.Up:
                case Direction.Down:
                    return GetNextChar(Direction.Left, x, y) != ' ' ? Direction.Left : Direction.Right;
                case Direction.Left:
                case Direction.Right:
                    return GetNextChar(Direction.Up, x, y) != ' ' ? Direction.Up : Direction.Down;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        private (int,int) GetNextCoordinate(Direction dir, int x, int y)
        {
            return dir switch
            {
                Direction.Up => (x, y - 1),
                Direction.Down => (x, y + 1),
                Direction.Left => (x - 1, y),
                Direction.Right => (x + 1, y),
                _ => (x,y),
            };
        }
        

        private char GetNextChar(Direction dir, int x, int y)
        {
            return dir switch
            {
                Direction.Up => (NetworkDiagram[y - 1])[x],
                Direction.Right => (NetworkDiagram[y])[x + 1],
                Direction.Down => (NetworkDiagram[y + 1])[x],
                Direction.Left => (NetworkDiagram[y])[x - 1],
                _ => ' '
            };
        }

        private int FindStart()
        {
            var x = 0;
            foreach (var c in NetworkDiagram[0])
            {
                if (c == '|')
                {
                    return x;
                }
                x++;
            }

            return -1;
        }

        private enum Direction
        {
            Up, Right, Down, Left
        }
    }
}