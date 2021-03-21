using System;

namespace AdventOfCode2017.days
{
    internal class Day22 : Day
    {
        private States[,] InfectedCluster { get; }
        private const int Size = 500;
        private (int,int) Position { get; set; }
        private Directions Direction { get; set; }
        private int Infections { get; set; }
        public Day22(string input, bool part1) : base(input, part1)
        {
            InfectedCluster = new States[Size,Size];
            Position = (Size / 2, Size / 2);
            Infections = 0;
            Direction = Directions.Up;
            InitializeCluster();
        }

        protected override string Solve1()
        {
            for (var i = 0; i < 10000; i++)
            {
                Burst(false);
            }

            return Infections.ToString();
        }
        
        protected override string Solve2()
        {
            for (var i = 0; i < 10000000; i++)
            {
                Burst();
            }

            return Infections.ToString();
        }

        private void Burst(bool evolved = true)
        {
            var x = Position.Item1;
            var y = Position.Item2;
            Turn(evolved, InfectedCluster[y,x]);

            var nextState = NextState(InfectedCluster[y,x], evolved);
            if (nextState == States.Infected)
            {
                Infections++;
            }

            InfectedCluster[y, x] = nextState; 
            Move();
        }

        private States NextState(States state, bool evolved)
        {
            if (evolved)
            {
                return state switch
                {
                    States.Clean => States.Weakened,
                    States.Weakened => States.Infected,
                    States.Infected => States.Flagged,
                    _ => States.Clean
                };
            }
            return state switch
            {
                States.Clean => States.Infected,
                _ => States.Clean,
            };
        }


        private void Move()
        {
            Position = Direction switch
            {
                Directions.Up => (Position.Item1, Position.Item2 - 1),
                Directions.Down => (Position.Item1, Position.Item2 + 1),
                Directions.Left => (Position.Item1 - 1, Position.Item2),
                Directions.Right => (Position.Item1 + 1, Position.Item2),
                _ => (Position.Item1, Position.Item2)
            };
        }
        
        private void Turn(bool evolved, States state)
        {
            if (!evolved)
            {
                Turn(state == States.Infected);
                return;
            }

            switch (state)
            {
                case States.Clean:
                    Turn(false);
                    break;
                case States.Infected:
                    Turn(true);
                    break;
                case States.Flagged:
                    Turn(true);
                    Turn(true);
                    break;
                case States.Weakened:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Turn(bool clockwise)
        {
            if (clockwise)
            {
                Direction = Direction switch
                {
                    Directions.Up => Directions.Right,
                    Directions.Right => Directions.Down,
                    Directions.Down => Directions.Left,
                    _ => Directions.Up
                };
                return;
            }
            Direction =  Direction switch
            {
                Directions.Up => Directions.Left,
                Directions.Left => Directions.Down,
                Directions.Down => Directions.Right,
                _ => Directions.Up
            };
        }
        
        private void InitializeCluster()
        {
            var mapCenter = Input.Split("\n");
            var height = mapCenter.Length;
            var width = mapCenter[0].Length;
            var y = (Size / 2) - (height / 2);
            var x = (Size / 2) - (width / 2);
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    InfectedCluster[y + i, x + j] = mapCenter[i][j] == '#'? States.Infected : States.Clean;
                }
            }
        }

        private enum Directions
        {
            Up, Right, Down, Left
        }
        private enum States
        {
            Clean, Weakened, Infected, Flagged
        }

    }
}