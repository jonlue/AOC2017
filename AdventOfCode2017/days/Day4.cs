using System.IO;

namespace AdventOfCode2017.days
{
    internal class Day4 : Day
    {
        public Day4(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input4.txt")
                .Replace("\r", "");
        }

        protected override string Solve1()
        {
            var validPassphrases = 0;
            foreach (var passphrase in Input.Split("\n"))
            {
                var valid = true;
                var words = passphrase.Split(" ");
                
                for (var i = 0; i < words.Length-1; i++)
                {
                    for (var j = i+1; j < words.Length; j++)
                    {
                        if (!words[i].Equals(words[j])) continue;
                        valid = false;
                        break;
                    }
                    if (!valid) break;
                }

                if (valid) validPassphrases++;
            }

            return validPassphrases.ToString();
        }

        protected override string Solve2()
        {
            var validPassphrases = 0;
            foreach (var passphrase in Input.Split("\n"))
            {
                var valid = true;
                var words = passphrase.Split(" ");
                
                for (var i = 0; i < words.Length-1; i++)
                {
                    for (var j = i+1; j < words.Length; j++)
                    {
                        if (words[i].Equals(words[j]))
                        {
                            valid = false;
                            break;
                        }
                        
                        var w2 = words[j];
                        
                        if (words[i].Length != words[j].Length) continue;
                        var letterMatch = 0;
                        foreach (var letter1 in words[i])
                        {
                            for (var k = 0; k< w2.Length; k++)
                            {
                                if (letter1 != w2[k]) continue;
                                letterMatch++;
                                w2 = w2.Remove(k, 1);
                                break;
                            }
                        }

                        if (letterMatch != words[i].Length) continue;
                        valid = false;
                        break;
                        
                    }
                    if (!valid) break;
                }

                if (valid) validPassphrases++;
            }

            return validPassphrases.ToString();
        }
    }
}