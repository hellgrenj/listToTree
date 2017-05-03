using System;
using System.Collections.Generic;
using System.Linq;

namespace listToTree
{
    class Program
    {

        static void Main(string[] args)
        {
            var nodes = new List<Node>();


            var rootNode = new Node();
            rootNode.Id = 1;
            rootNode.ParentId = null;
            rootNode.Children = new List<Node>();

            var secondRootNode = new Node();
            secondRootNode.Id = 2;
            secondRootNode.ParentId = null;
            secondRootNode.Children = new List<Node>();

            // add 2 root nodes 
            nodes.Add(rootNode);
            nodes.Add(secondRootNode);

            // add some children
            nodes.Add(new Node
            { // second level
                Id = 3,
                ParentId = 1, // root 1
                Children = new List<Node>()
            });

            nodes.Add(new Node
            { // third level
                Id = 4,
                ParentId = 3,
                Children = new List<Node>()
            });

            nodes.Add(new Node
            { // second level
                Id = 5,
                ParentId = 2, // root 2
                Children = new List<Node>()
            });

            nodes.Add(new Node
            { // third level
                Id = 6,
                ParentId = 5,
                Children = new List<Node>()
            });

            nodes.Add(new Node
            { // fourth level
                Id = 7,
                ParentId = 6,
                Children = new List<Node>()
            });
            
            nodes.Add(new Node
            { // second level on first root node
                Id = 8,
                ParentId = 1, // root 1 
                Children = new List<Node>()
            });

            var tree = ListToTree(nodes);
            Console.WriteLine("the tree:");
            Print(tree[0], tree[0].Id.ToString(), 1);
            Print(tree[1], tree[1].Id.ToString(), 1);


            // Expected output
            /*
            the tree:
            1
             3
              4
            1
               8
            2
             5
              6
               7
             */
            
        }

        public static List<Node> ListToTree(IEnumerable<Node> list)
        {
            var lookup = list.ToDictionary(n => n.Id, n => n);
            var rootNodes = new List<Node>();
            foreach (var node in list) 
            {
                if (node.ParentId.HasValue)
                {
                    //add node to its parent
                    Node parent = lookup[node.ParentId.Value];
                    parent.Children.Add(node);
                }
                else
                {
                    rootNodes.Add(node);
                }
            }
            return rootNodes;
        }

        public static void Print(Node node, string result, int level)
        {
            if (node.Children == null || node.Children.Count == 0)
            {
                Console.WriteLine(result);
                return;
            }
            var indent = "\n";
            foreach (var child in node.Children)
            {
                for(var i = 0; i < level; i++) {
                    indent += " ";
                }
                level = level + 1;
                Print(child, result + indent + child.Id, level);
            }
        }
    }

    public class Node
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public List<Node> Children { get; set; }
    }
}
