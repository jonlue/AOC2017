using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2017.util;

namespace AdventOfCode2017.days
{
    internal class Day7 : Day
    {
        private string[] Programs { get; set; }
        public Day7(bool part1) : base(part1)
        {
            Input = File
                .ReadAllText("C:\\Users\\Jonas\\RiderProjects\\AOC2017\\AdventOfCode2017\\resources\\input7.txt")
                .Replace("\r", "");
            Programs = Input.Split("\n");
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
            var nodesWeight = new Dictionary<string, int>(); 
            var nodesChildren = new Dictionary<string, string>(); 
            var rootName = Solve1();

            foreach (var line in Programs)
            {
                var name = line.Split(" ")[0];
                var weight = int.Parse(line.Substring(line.IndexOf('(')+1).Split(")")[0]);
                if (line.Contains("->"))
                {
                    nodesChildren.Add(name,line.Split("->")[1].Replace(" ",""));
                }
                nodesWeight.Add(name, weight);
            }

            var root = BuildTree(new Node(rootName, nodesWeight[rootName]), nodesChildren, nodesWeight);

            return "";
        }

        private Node BuildTree(Node node, Dictionary<string, string> nodesChildren, Dictionary<string, int> nodesWeight)
        {
            if (!nodesChildren.ContainsKey(node.Name)) return node;
            foreach (var childName in nodesChildren[node.Name].Split(","))
            {
                node.AddChild(
                    BuildTree(new Node(childName, nodesWeight[childName]),nodesChildren,nodesWeight));
            }

            return node;
        }
    }
}