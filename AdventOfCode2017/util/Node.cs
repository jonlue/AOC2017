using System.Collections.Generic;

namespace AdventOfCode2017.util
{
    public class Node
    {
        
        public string Name { get; set; }
        public int Weight { get; set; }
        public List<Node> Children { get; set; }

        public Node(string name, int weight,  List<Node> children)
        {
            Name = name;
            Weight = weight;
            Children = children;
        }
        
        public Node(string name, int weight)
        {
            Name = name;
            Weight = weight;
            Children = null;
        }

        public void AddChild(Node child)
        {
            Children.Add(child);
        }
    }
}