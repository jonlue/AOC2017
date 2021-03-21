using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.days
{
    internal class Day21 : Day
    {
        private const string Start = ".#." + "\n" +
                                     "..#" + "\n" +
                                     "###";

        private Dictionary<string,string> Rules { get; set; }

        public Day21(string input, bool part1) : base(input, part1)
        {
            Input = Input.Replace(" ", "");
            InitializeRules();
        }

        protected override string Solve1()
        {
            return EnhanceImage(Start, 5)
                .Count(c => c == '#')
                .ToString();
        }

        protected override string Solve2()
        {
            Console.WriteLine("This takes a long time :(");
            Console.WriteLine("Answer to this input:\t2368161");
            return EnhanceImage(Start, 18)
                .Count(c => c == '#')
                .ToString();
        }

        private string EnhanceImage(string image, int times)
        {
            for (var i = 0; i < times; i++)
            {
                List<string> subImages;
                if (image.Split("\n").Length % 2 == 0)
                {
                    subImages = SplitImage(image, 2);
                }
                else
                {
                    subImages = SplitImage(image, 3);
                }


                image = JoinImages(EnhanceSubImages(subImages));
                Console.WriteLine(i + "\t" + image.Length);
            }

            return image;
        }
        
        // enhance by remembering repeating offenders
        // or maybe easier by just counting on lights and how many they result into
        private string JoinImages(IReadOnlyList<string[]> subImages)
        {
            var size = subImages[0][0].Length;
            var sideLenght = (int) Math.Sqrt(subImages.Count);

            var res = "";
            for (var i = 0; i < subImages.Count; i+= sideLenght)
            {
                for (var y = 0; y < size; y++)
                {
                    for (var x = 0; x < sideLenght; x++)
                    {
                        res += subImages[i+x][y];
                    }
                    res += "\n";
                }
            }

            return res[..^1];
        }

        private List<string> SplitImage(string image, int size)
        {
            
            var subImages = new List<string>();
            var temp = image.Split("\n");

            for (var y = 0; y < temp.Length; y += size)
            {
                for(var x = 0; x < temp[y].Length; x+= size)
                {
                    var subImage = "";
                    for (var i = 0; i < size; i++)
                    {
                        subImage += temp[y+ i].Substring(x, size) + "\n";
                    }
                    subImages.Add(subImage[..^1]);
                }
            }

            return subImages;
        }

        private List<string[]> EnhanceSubImages(IEnumerable<string> subImage)
        {
            var enhancedImages = new List<string[]>();
            foreach (var image in subImage)
            {
                foreach (var (key, value) in Rules)
                {
                    if (!image.Equals(key)) continue;
                    enhancedImages.Add(value.Split("\n"));
                    break;
                }    
            }
            return enhancedImages;
        }
        
        private void InitializeRules()
        {
            Rules = new Dictionary<string, string>();
            foreach (var rule in Input.Split("\n"))
            {
                var newRule = rule.Replace("/", "\n").Split("=>");
                var input = newRule[0];
                var output = newRule[1];

                for (var i = 0; i < 4; i++)
                {
                    Rules.TryAdd(Rotate(input,i),output);
                }

                input = Flip(input);
                for (var i = 0; i < 4; i++)
                {
                    Rules.TryAdd(Rotate(input,i),output);
                }
            }
        }

        private string Flip(string input)
        {
            var temp = input.Split("\n");
            var top = temp[0];
            var bottom = temp[^1];

            var res = bottom + "\n";
            if (temp[0].Length == 3)
            {
                res += temp[1] + "\n";
            }

            res += top;
            return res;
        }

        private string Rotate(string input, int count)
        {
            if (count == 0)
            {
                return input;
            }
            var original = input.Split("\n");
            var len = original[0].Length;
            var res = "";
            for (var i = 0; i < count; i++)
            {
                res = "";
                for (var k = 0; k < len; k++)
                {
                    for (var j = len-1; j >= 0; j--)
                    {
                        res += original[j][k];
                    }
                    res += "\n";
                }

                original = res.Split("\n");
            }

            return res[0..^1];
        }
    }
}