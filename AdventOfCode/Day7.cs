using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day7
    {
        public static void Solve(string inputPath, bool isP2 = false)
        {
            if (Validate())
            {
                List<string> input = HelperFunctions.ReadFile(inputPath);
                if (isP2)
                {
                    string p2Result = PartOne(input);
                    Console.WriteLine($"Solution is {p2Result}");
                }
                else
                {
                    string result = PartOne(input);
                    Console.WriteLine($"Solution is {result}");
                }
            }
            else
            {
                Console.WriteLine("Tests failed. Did not run day 2.");
            }
        }

        public static bool Validate()
        {

            bool result = false;
            List<string> input = HelperFunctions.ReadFile("Data/D7P1Test.txt");
            string resultP1 = PartOne(input);
            if(resultP1 == "CABDFE")
            {
                result = true;
            }
            
            return result;
        }

        public static string PartOne(List<string> input)
        {
            List<Node> nodes = ParseIntoNodes(input);
            // Find root of tree
            List<Node> nextNodes = nodes.Where(p => p.GetRequiredNodeCount() == 0).ToList();
            Node root = nextNodes.OrderBy(p => p.Name).First();
            nextNodes.Remove(root);
            nextNodes = DistinctAddRange(nextNodes, root.GetNextNodes());
            StringBuilder result = new StringBuilder();
            result = TraverseNodes(root, nextNodes, ref result);

            return result.ToString();
        }

        /// <summary>
        /// Iterate through Nodes
        /// </summary>
        /// <param name="nodes"></param>
        public static StringBuilder TraverseNodes(Node root, List<Node>nextNodes, ref StringBuilder result)
        {
            result = result.Append(root.Name);
            //Console.Write(root.Name);
            if (nextNodes.Count == 0)
            {
                return result;
            }
            else
            {
                List<Node> eligibleNodes = new List<Node>();
                foreach (Node node in nextNodes)
                {
                    node.RemoveRequiredNode(root);
                    if (node.GetRequiredNodeCount() == 0)
                    {
                        eligibleNodes.Add(node);
                    }
                }

                eligibleNodes = eligibleNodes.OrderBy(p => p.Name).ToList();

                Node nextNode = nextNodes.First(p => p.Name == eligibleNodes.First().Name);
                root = nextNode;
                nextNodes.Remove(root);
                nextNodes = DistinctAddRange(nextNodes, root.GetNextNodes());
                return TraverseNodes(nextNode, nextNodes, ref result);

            }

        }

        public static List<Node> DistinctAddRange(List<Node> list1, List<Node> list2)
        {
            foreach(Node node in list2)
            {
                if (!list1.Contains(node))
                {
                    list1.Add(node);
                }
            }

            return list1;
        }

        /// <summary>
        /// Parse string input into node structure
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<Node> ParseIntoNodes(List<string> input)
        {
            List<Node> nodes = new List<Node>();
            foreach(string row in input)
            {
                Regex rowRegex = new Regex("Step ([A-Z]) must be finished before step ([A-Z]) can begin.");
                Match m = rowRegex.Match(row);

                if (m.Success)
                {
                    string nodeName = m.Groups[1].ToString();
                    string nextName = m.Groups[2].ToString();
                    Node currentNode;
                    Node nextNode;

                    // Parse and set next nodes
                    if (nodes.Count(p => p.Name == nodeName) == 0)
                    {
                        currentNode = new Node(nodeName);
                        nodes.Add(currentNode);
                    }
                    else
                    {
                        currentNode = nodes.FirstOrDefault(p => p.Name == nodeName);
                    }

                    // Parse and set required nodes
                    if (nodes.Count(p => p.Name == nextName) == 0)
                    {
                        nextNode = new Node(nextName);
                        nodes.Add(nextNode);
                    }
                    else
                    {
                        nextNode = nodes.FirstOrDefault(p => p.Name == nextName);
                    }

                    nextNode.AddRequiredNode(currentNode);
                    currentNode.SetNextNode(nextNode);

                }

            }
            return nodes;
        }
    }

    class Node
    {
        public string Name;
        private List<Node> NextNodes;
        private List<Node> RequiredNodes;

        public Node(string name)
        {
            Name = name;
            NextNodes = new List<Node>();
            RequiredNodes = new List<Node>();
        }

        public void SetNextNode(Node nextNode)
        {
            if (!NextNodes.Contains(nextNode))
            {
                NextNodes.Add(nextNode);
            }
        }

        public List<Node> GetNextNodes()
        {
            return NextNodes;
        }

        public int GetRequiredNodeCount()
        {
            return RequiredNodes.Count;
        }

        public List<Node> AddRequiredNode(Node node)
        {
            if (!RequiredNodes.Contains(node))
            {
                RequiredNodes.Add(node);
            }

            return RequiredNodes;
        }

        /// <summary>
        /// Remove required node from list
        /// returns false if there are no more required nodes
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public void RemoveRequiredNode(Node node)
        {
            if (RequiredNodes.Contains(node))
            {
                RequiredNodes.Remove(node);
            }
        }

    }
}
