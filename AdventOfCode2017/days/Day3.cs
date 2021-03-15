using System;
using System.IO;

namespace AdventOfCode2017.days
{
    internal class Day3 : Day
    {
        private int Goal { get; set; }
        public Day3(bool part1) : base(part1)
        {
            Input = File.ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input3.txt");
            Goal = int.Parse(Input);
        }

        protected override string Solve1()
        {
            var memory = new int[1000, 1000];
            var x = 499;
            var y = 499;

            var count = 1;
            memory[x, y] = count;


            while (count<Goal)
            {
                //go right
                do
                {
                    if(count>=Goal) break;
                    x++;
                    count++;
                    memory[x, y] = count;
                } while (memory[x, y - 1] != 0 && count < Goal);

                //go up
                do
                {
                    if(count>=Goal) break;
                    y--;
                    count++;
                    memory[x, y] = count;
                } while (memory[x - 1, y] != 0 && count < Goal);

                //go left
                do
                {
                    if(count>=Goal) break;
                    x--;
                    count++;
                    memory[x, y] = count;
                } while (memory[x, y + 1] != 0 && count < Goal);

                //go down
                do
                {
                    if(count>=Goal) break;
                    y++;
                    count++;
                    memory[x, y] = count;
                } while (memory[x + 1, y] != 0 && count < Goal);
            }

            return (Math.Abs(499 - x) + Math.Abs(499 - y)).ToString();
        }

        protected override string Solve2()
        {
            var memory = new int[1000, 1000];
            var x = 499;
            var y = 499;

            var count = 1;
            memory[x, y] = count;


            while (count<Goal)
            {
                //go right
                do
                {
                    if(count>=Goal) break;
                    x++;
                    count = SumNeighbors(x, y, memory);
                    memory[x, y] = count;
                } while (memory[x, y - 1] != 0 && count < Goal);

                //go up
                do
                {
                    if(count>=Goal) break;
                    y--;
                    count = SumNeighbors(x, y, memory);
                    memory[x, y] = count;
                } while (memory[x - 1, y] != 0 && count < Goal);

                //go left
                do
                {
                    if(count>=Goal) break;
                    x--;
                    count = SumNeighbors(x, y, memory);
                    memory[x, y] = count;
                } while (memory[x, y + 1] != 0 && count < Goal);

                //go down
                do
                {
                    if(count>=Goal) break;
                    y++;
                    count = SumNeighbors(x, y, memory);
                    memory[x, y] = count;
                } while (memory[x + 1, y] != 0 && count < Goal);
            }

            return count.ToString();
        }

        private int SumNeighbors(int x, int y, int[,] memory)
        {
            var sum = 0;
            sum += memory[x - 1, y - 1];
            sum += memory[x, y - 1];
            sum += memory[x + 1, y - 1];
            
            sum += memory[x -1, y];
            sum += memory[x + 1, y];
            
            sum += memory[x - 1, y + 1];
            sum += memory[x, y + 1];
            sum += memory[x + 1, y + 1];
            
            return sum;
        }
    }
}