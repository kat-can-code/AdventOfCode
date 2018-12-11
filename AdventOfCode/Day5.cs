using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day5
    {
        public static void Solve(string inputPath, bool isP2 = false)
        {
            if (Validate())
            {
                List<string> input = HelperFunctions.ReadFile(inputPath);
                if (isP2)
                {
                    int result = PartTwo(input);
                    Console.WriteLine($"Solution is {result}");
                }
                else
                {
                    int result = PartOne(input);
                    Console.WriteLine($"Solution is {result}");
                }
            }
            else
            {
                Console.WriteLine("Tests failed. Did not run day 2.");
            }
        }

        public static int PartTwo(List<string> input)
        {
            string line = input.First();
            List<string> stacks = new List<string>();

            for(char i = 'a'; i <= 'z'; i++)
            {
                string charRemoved = Regex.Replace(line, i.ToString(), "", RegexOptions.IgnoreCase);
                string stack = BuildStack(charRemoved);
                stacks.Add(stack);
            }

            string result = stacks.OrderBy(p => p.Length).First();
            return result.Length;
        }



        public static int PartOne(List<string> input)
        {
            string line = input.First();
            string stack = BuildStack(line);
            return stack.Length;
        }

        public static string BuildStack(string input)
        {
            Stack stack = new Stack();
            int i = 0;
            while (i < input.Length)
            {
                if (stack.GetSize() == 0)
                {
                    stack.Push(input[i]);
                }
                else if (IsOppositeCase(input[i], stack.GetTopChar()))
                {
                    stack.Pop();
                }
                else
                {
                    stack.Push(input[i]);
                }
                i++;
            }

            return stack.ToString();
        }

        public static bool IsOppositeCase(char letter1, char letter2)
        {
            bool result = false;
            if (char.IsLower(letter1))
            {
                if (letter2 == char.ToUpper(letter1))
                {
                    result = true;
                }
            }
            else
            {
                if (letter2 == char.ToLower(letter1))
                    result = true;
            }

            return result;
        }


        public static bool Validate()
        {
            bool testResult = false;
            List<string> input = new List<string>(){ "dabAcCaCBAcCcaDA" };
            int resultP1 = PartOne(input);
            if(resultP1 == 10)
            {
                testResult = true;
            }

            int resultP2 = PartTwo(input);
            if (resultP1 == 4)
            {
                testResult = true;
            }

            return testResult;
        }
    }

    class Stack {
        StringBuilder stack;

        public Stack()
        {
            stack = new StringBuilder();
        }

        public StringBuilder Push(char token)
        {
            return stack.Append(token);
        }

        public char Pop()
        {
            char top = stack[stack.Length-1];
            stack.Length--;
            return top;
        }

        public int GetSize()
        {
            return stack.Length;
        }

        public char GetTopChar()
        {
            return stack[stack.Length-1];
        }

        public override string ToString()
        {
            return stack.ToString();
        }
    }
}
