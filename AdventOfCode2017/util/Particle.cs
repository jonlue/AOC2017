using System;
using System.Numerics;

namespace AdventOfCode2017.util
{
    public class Particle
    {
        public Vector3 Position { get; set; }
        private Vector3 Velocity { get; set; }
        private Vector3 Acceleration { get; }

        public Particle(Vector3 position, Vector3 velocity, Vector3 acceleration)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
        }
        
        public Particle(int[] position, int[] velocity, int[] acceleration)
        {
            Position = new Vector3(position[0], position[1],position[2]);
            Velocity = new Vector3(velocity[0],velocity[1],velocity[2]);
            Acceleration = new Vector3(acceleration[0],acceleration[1],acceleration[2]);
        }

        public void MakeSteps(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                Velocity += Acceleration;
                Position += Velocity;
            }
        }

        public void MakeStep()
        {
            Velocity += Acceleration;
            Position += Velocity;
        }

        public void FakeSteps(int times)
        {
            times = Math.Max(10000, times);
            Position += Acceleration * times;
        }

        public int GetManhattanDistance(Vector3 point = default)
        {
            return (int) (Math.Abs(Position.X - point.X) + Math.Abs(Position.Y - point.Y) + Math.Abs(Position.Z - point.Z));
        }
    }
}