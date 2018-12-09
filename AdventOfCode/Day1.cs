using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day1
    {
        /// <summary>
        /// Solve day 1's puzzles
        /// </summary>
        /// <param name="inputFilePath"></param>
        /// <returns></returns>
        public static void Solve(string inputFilePath, bool isP2 = false)
        {
            if (Validate())
            {
                List<string> input = HelperFunctions.ReadFile(inputFilePath);
                if (input != null)
                {
                    Console.WriteLine($"Solution is: " + GetOutput(input, isP2));
                }
                else
                {
                    Console.WriteLine($"File at {inputFilePath} was null.");
                }
            }
            else
            {
                Console.WriteLine("Tests failed. Did not run day 1.");
            }
        }

        /// <summary>
        /// Do the things
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static int GetOutput(List<string> input, bool isP2 = false)
        {
            int output = 0;

            // Initialize P2
            Dictionary<int, int> p2 = new Dictionary<int, int>();
            p2.Add(output, output);
            int maxIteration = 500;
            int i = 0;
            while (i < maxIteration)
            {
                foreach (string line in input)
                {
                    string tempLine = line.Trim();
                    char operation = tempLine[0];
                    int value = Convert.ToInt32(tempLine.Substring(1));
                    if (operation == '-')
                    {
                        output -= value;
                    }
                    else if (operation == '+')
                    {
                        output += value;
                    }
                    else
                    {
                        throw new Exception($"Error has occurred during calculation due to line {tempLine}");
                    }

                    //check for p2
                    if (isP2)
                    {
                        // P2 duplicate found
                        if (p2.ContainsKey(output))
                        {
                            return output;
                        }
                        else
                        {
                            p2.Add(output, output);
                        }
                    }
                }

                // P1 is found
                if (!isP2)
                {
                    return output;
                }

                i++;

            }

            //P2 no dupe found
            throw new Exception($"D1P2 no duplicate found in {maxIteration} iterations");
        }

        

        /// <summary>
        /// Test Day 1 algorithm against known values
        /// </summary>
        /// <returns></returns>
        public static bool Validate()
        {
            List<string> testArray = new List<string>(){ "+1", "+1", "+1" };
            int testResult = GetOutput(testArray);
            if(testResult != 3)
            {
                return false;
            }

            testArray = new List<string>() { "+1", "-2", "+3", "+1" };
            testResult = GetOutput(testArray);
            if (testResult != 3)
            {
                return false;
            }

            testResult = GetOutput(testArray, true);
            if (testResult != 2)
            {
                return false;
            }

            testArray = new List<string>{ "+1", "+1", "-2" };
            testResult = GetOutput(testArray);
            if (testResult != 0)
            {
                return false;
            }

            testArray = new List<string>{ "-1", "-2", "-3" };
            testResult = GetOutput(testArray);
            if (testResult != -6)
            {
                return false;
            }

            testArray = new List<string>{"+1","-1" };
            testResult = GetOutput(testArray, true);
            if (testResult != 0)
            {
                return false;
            }

            testArray = new List<string> { "+3", "+3", "+4", "-2", "-4" };
            testResult = GetOutput(testArray, true);
            if (testResult != 10)
            {
                return false;
            }

            testArray = new List<string> { "-6", "+3", "+8", "+5", "-6" };
            testResult = GetOutput(testArray, true);
            if (testResult != 5)
            {
                return false;
            }

            testArray = new List<string> { "+7", "+7", "-2", "-7", "-4" };
            testResult = GetOutput(testArray, true);
            if (testResult != 14)
            {
                return false;
            }

            return true;
        }
    }
}
