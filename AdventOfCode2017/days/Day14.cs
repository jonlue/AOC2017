using System.IO;
using System.Linq;
using AdventOfCode2017.util;

namespace AdventOfCode2017.days
{
    internal class Day14 : Day
    {
        private KnotHash KnotHash { get; }

        public Day14(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input14.txt")
                .Replace("\r", "");
            KnotHash = new KnotHash(2);
        }

        protected override string Solve1()
        {
            return (Defrag()
                    .Count(num => num == '1'))
                .ToString();
        }

        private string Defrag()
        {
            var res = "";
            for (var i = 0; i < 128; i++)
            {
                var newInput = Input + "-" + i;
                KnotHash.Input = newInput;
                KnotHash.RunHash();
                res += KnotHash.Hash + "\n";
            }

            return res;
        }

        protected override string Solve2()
        {
            var grid = new char[128, 128];
            var regions = 0;
            var text = Defrag().Split("\n");

            for (var i = 0; i < 128; i++)
            {
                for (var j = 0; j < 128; j++)
                {
                    grid[i, j] = text[i][j];
                }
            }

            for (var i = 0; i < 128; i++)
            {
                for (var j = 0; j < 128; j++)
                {
                    if (grid[i, j] == '1')
                    {
                        regions++;
                        CheckRegions(i, j, grid);
                    }
                }
            }


            return regions.ToString();
        }

        private void CheckRegions(int y, int x, char[,] grid)
        {
            grid[y, x] = '0';
            CheckNeighbors(y, x, grid);
        }

        private void CheckNeighbors(int y, int x, char[,] grid)
        {
            while (true)
            {
                if (x > 0 && grid[y, x - 1] == '1')
                {
                    grid[y, x - 1] = '0';
                    CheckNeighbors(y, x - 1, grid);
                }

                if (x + 1 < grid.GetLength(1) && grid[y, x + 1] == '1')
                {
                    grid[y, x + 1] = '0';
                    CheckNeighbors(y, x + 1, grid);
                }

                if (y > 0 && grid[y - 1, x] == '1')
                {
                    grid[y - 1, x] = '0';
                    CheckNeighbors(y - 1, x, grid);
                }

                if (y + 1 < grid.GetLength(1) && grid[y + 1, x] == '1')
                {
                    grid[y + 1, x] = '0';
                    y += 1;
                    continue;
                }

                break;
            }
        }
        
    }
}