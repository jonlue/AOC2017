using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.util
{
    public class KnotHash
    {
        private int Position { get; set; }
        private int SkipSize { get; set; }
        private string Input { get; }
        public string Hash { get; private set; }
        public List<int> Knots { get; private set; }
        
        private const string EndLengths = "17,31,73,47,23";
        private const int Length = 256;
        private const int HashLength = 16;

        public KnotHash(int position, int skipSize, string input)
        {
            Position = position;
            SkipSize = skipSize;
            Input = ConvertInput(input);
            InitKnots();
        }
        
        public KnotHash(int position, int skipSize)
        {
            Position = position;
            SkipSize = skipSize;
            Input = "";
            InitKnots();
        }

        public KnotHash(string input, bool convertInput)
        {
            Position = 0;
            SkipSize = 0;
            Input = convertInput ? ConvertInput(input) : input;
            InitKnots();
        }

        private void InitKnots()
        {
            Knots = new List<int>();
            Knots.AddRange(Enumerable.Range(0, Length));
        }

        private static string ConvertInput(string str)
        {
            return str.Aggregate("", (current, c) => current + ((int) c + ",")) + EndLengths;
        }

        public void GenerateHash(string format)
        {
            var deepHash = "";
            for (var i = 0; i < (Length / HashLength); i++)
            {
                var value = Knots[i*HashLength];
                for (var j = (i * HashLength) + 1; j < (i * HashLength + HashLength); j++)
                {
                    value ^= Knots[j];
                }

                deepHash += value.ToString(format).ToLower();
            }

            Hash = deepHash;
        }


        public void RunHash()
        {
            foreach (var length in Array.ConvertAll(Input.Split(","),int.Parse))
            {
                if(length > Knots.Count) continue;
                //Reverse
                if (length != 1)
                {
                    // Snap around list  
                    if (Position + length > Knots.Count)
                    {
                        var lengthEnd = Knots.Count - Position;

                        var temp1 = Knots.GetRange(Position, lengthEnd);
                        temp1.AddRange(Knots.GetRange(0, length - lengthEnd));
                        temp1.Reverse();

                        Knots.RemoveRange(Position, lengthEnd);
                        Knots.InsertRange(Position, temp1.GetRange(0, lengthEnd));

                        temp1.RemoveRange(0, lengthEnd);

                        Knots.RemoveRange(0, length - lengthEnd);
                        Knots.InsertRange(0, temp1);
                    }
                    else
                    {
                        var temp = Knots.GetRange(Position, length);
                        temp.Reverse();

                        Knots.RemoveRange(Position, length);
                        Knots.InsertRange(Position, temp);
                    }
                }

                //Move
                Position = (Position + length + SkipSize) % Knots.Count;

                //Increase
                SkipSize++;
            }
        }
    }
}