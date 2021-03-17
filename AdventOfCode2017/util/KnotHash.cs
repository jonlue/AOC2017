using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.util
{
    public class KnotHash
    {
        private int Position { get; set; }
        private int SkipSize { get; set; }
        public string Input { get; set; }
        private int[] ConvertedInput { get; set; }
        public string Hash { get; private set; }
        private bool Conversion { get; }
        public List<int> Knots { get; private set; }
        public int FormatBase { get; set; }

        private const string EndLengths = "17,31,73,47,23";
        private const int Length = 256;
        private const int HashLength = 16;

        public KnotHash(int formatBase = 16)
        {
            Position = 0;
            SkipSize = 0;
            Input = "";
            Conversion = true;
            FormatBase = formatBase;
            InitKnots();
        }

        public KnotHash(string input, bool convertInput = true, int formatBase = 16)
        {
            Position = 0;
            SkipSize = 0;
            Conversion = convertInput;
            Input = input;
            FormatBase = formatBase;
            InitKnots();
        }

        private void InitKnots()
        {
            Knots = new List<int>();
            Knots.AddRange(Enumerable.Range(0, Length));
        }

        private void ConvertInput(string str)
        {
            ConvertedInput = Array.ConvertAll(
                (str.Aggregate("", (current, c) => current + ((int) c + ","))
                 + EndLengths).Split(","),
                int.Parse);
        }

        private void GenerateHash()
        {
            var deepHash = "";
            for (var i = 0; i < (Length / HashLength); i++)
            {
                var value = Knots[i * HashLength];
                for (var j = (i * HashLength) + 1; j < (i * HashLength + HashLength); j++)
                {
                    value ^= Knots[j];
                }

                var t = Convert.ToString(value, FormatBase);
                
                while (t.Length < Math.Ceiling(Math.Log(Length, FormatBase)))
                {
                    t = "0" + t;
                }
                deepHash += t;
            }
            
            Hash = deepHash;
        }


        public void RunHash(int times = 64)
        {
            if (Conversion)
            {
                ConvertInput(Input);
            }
            else
            {
                ConvertedInput = Array.ConvertAll(Input.Split(","), int.Parse);
            }

            for (var i = 0; i < times; i++)
            {
                HashStep();
            }

            GenerateHash();
            Reset();
        }

        private void HashStep()
        {
            foreach (var length in ConvertedInput)
            {
                if (length > Knots.Count) continue;
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

        private void Reset()
        {
            Position = 0;
            SkipSize = 0;
            InitKnots();
        }
    }
}