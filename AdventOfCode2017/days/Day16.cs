using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.days
{
    internal class Day16 : Day
    {

        private string[] DanceMoves { get; }
        private const char Spin = 's';
        private const char Exchange = 'x';
        private const char Partner = 'p';
        private char[] Programs { get; set; }
        public Day16(string input, bool part1) : base(input, part1)
        {
            DanceMoves = Input.Split(",");
            Programs = "abcdefghijklmnop".ToCharArray();
        }

        protected override string Solve1()
        {
            Dance();
            return GetOrder();
        }

        protected override string Solve2()
        {
            var order = new Dictionary<string, int> {{GetOrder(), 0}};
            var count = 0;
            while (true)
            {
                count++;
                Dance();
                var t = GetOrder();
                if (order.ContainsKey(t))
                {
                    break;
                }

                order.Add(t,count);
            }

            var searchedIndex = (1000000000 % count);
            var res = "";
            foreach (var key in order.Keys.Where(key => order[key] == searchedIndex))
            {
                res = key;
            }
            return res;
        }

        private void Dance()
        {
            foreach (var move in DanceMoves)
            {
                Move(move);
            }
        }


        private void Move(string move)
        {
            var pos1 = -1;
            var pos2 = -1;
            char p;
            var values = move[1..].Split("/");
            switch (move[0])
            {
                    
                case(Spin):
                    var count = int.Parse(values[0]);
                    var temp = Programs[^count..];
                    for (var i = Programs.Length - 1-count; i >= 0; i--)
                    {
                        Programs[i + count] = Programs[i];
                    }

                    for (int i = 0; i < count; i++)
                    {
                        Programs[i] = temp[i];
                    }
                    break;
                case (Exchange):
                    pos1 = int.Parse(values[0]);
                    pos2 = int.Parse(values[1]);
                    
                    p = Programs[pos1];
                    Programs[pos1] = Programs[pos2];
                    Programs[pos2] = p;
                    
                    break;
                case(Partner):
                    var p1 = values[0];
                    var p2 = values[1];
                    for (var i = 0; i < Programs.Length; i++)
                    {
                        if (Programs[i] == p1[0])
                        {
                            pos1 = i;
                        }

                        if (Programs[i] == p2[0])
                        {
                            pos2 = i;
                        }
                    }
                    p = Programs[pos1];
                    Programs[pos1] = Programs[pos2];
                    Programs[pos2] = p;
                    break;
            }
        }

        private string GetOrder()
        {
            return Programs.Aggregate("", (current, c) => current + c);
        }
    }
}