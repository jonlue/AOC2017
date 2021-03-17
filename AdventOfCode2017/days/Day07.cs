using System.Collections.Generic;
using System.Linq;
using AdventOfCode2017.util;

namespace AdventOfCode2017.days
{
    internal class Day07 : Day
    {
        private string[] Programs { get; }
        private Dictionary<string, int> NodesWeight { get; }
        private Dictionary<string, string> NodesChildren { get; }
        public Day07(string input, bool part1) : base(input, part1)
        {
            Programs = Input.Split("\n");
            NodesWeight = new Dictionary<string, int>();
            NodesChildren = new Dictionary<string, string>();
        }

        protected override string Solve1()
        {
            foreach (var line in Programs)
            {
                if (!line.Contains("->")) continue;
                var programName = line.Split(" ")[0];
                if (Programs.Count(l => l.Contains(programName)) == 1)
                {
                    return programName;
                }
            }

            return "-1";
        }

        protected override string Solve2()
        {
            var rootName = Solve1();

            foreach (var line in Programs)
            {
                var name = line.Split(" ")[0];
                var weight = int.Parse(line.Substring(line.IndexOf('(')+1).Split(")")[0]);
                if (line.Contains("->"))
                {
                    NodesChildren.Add(name,line.Split("->")[1].Replace(" ",""));
                }
                NodesWeight.Add(name, weight);
            }

            var root = BuildTree(new Node(rootName, NodesWeight[rootName], (Node)null));
            SumWeight(root);
            
            return GetWrongWeight(root,0).ToString();
        }

        private Node BuildTree(Node node)
        {
            if (!NodesChildren.ContainsKey(node.Name)) return node;
            foreach (var childName in NodesChildren[node.Name].Split(","))
            {
                node.AddChild(
                    BuildTree(new Node(childName, NodesWeight[childName],node)));
            }

            return node;
        }

        private int SumWeight(Node n)
        {
            foreach (var child in n.Children)
            {
                n.SumWeight += SumWeight(child);
            }

            return n.SumWeight;
            /*
            if (n.Children.Count != 0)
            {
                foreach (var child in n.Children)
                {
                    n.SumWeight += SumWeight(child);
                }
            }
            return  n.Weight;
            */
        }

        private int GetWrongWeight(Node node, int difference)
        {
            var weightCount = new Dictionary<int, int>();
            foreach (var child in node.Children)
            {
                if (!weightCount.ContainsKey(child.SumWeight))
                {
                    weightCount.Add(child.SumWeight, 0);
                }

                var v = weightCount[child.SumWeight];
                weightCount.Remove(child.SumWeight);
                weightCount.Add(child.SumWeight,v+1);
            }

            var wrongWeight = weightCount.Keys.FirstOrDefault(key => weightCount[key] == 1);
            
            if (wrongWeight == 0)
            {
                return node.Weight + difference;
            }
            
            difference = weightCount.Keys.FirstOrDefault(key => weightCount[key] > 1) - wrongWeight;

            var t = node.Children.FirstOrDefault(child => child.SumWeight == wrongWeight);

            return GetWrongWeight(t, difference);
        }
    }
}