using System.Collections.Generic;

namespace AdventOfCode2017.util
{
    
    // Used for Day 7
    public class Node
    {
        
        public string Name { get; }
        public int Weight { get; }
        public List<Node> Children { get; }
        private Node Parent { get; }
        public int SumWeight { get; set; }

        public Node(string name, int weight,  List<Node> children)
        {
            Name = name;
            Weight = weight;
            Children = children;
            Parent = null;
            SumWeight = weight;
        }
        
        public Node(string name, int weight, Node parent)
        {
            Name = name;
            Weight = weight;
            Parent = parent;
            Children = new List<Node>();
            SumWeight = weight;
        }

        public Node(Node n)
        {
            Name = n.Name;
            Weight = n.Weight;
            Children = n.Children;
            Parent = n.Parent;
            SumWeight = n.SumWeight;
        }

        public void AddChild(Node child)
        {
            Children.Add(child);
        }
    }
}