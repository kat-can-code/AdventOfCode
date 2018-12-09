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
        /// Solve day 1's puzzle
        /// </summary>
        /// <param name="inputFilePath"></param>
        /// <returns></returns>
        public static int Solve(string inputFilePath)
        {
            if (Validate())
            {
                string[] input = HelperFunctions.ReadFile(inputFilePath);
                if(input != null)
                {
                    return GetOutput(input);
                }
                else
                {
                    throw new Exception($"Day1. File at {inputFilePath} was null.");
                }
            }
            else
            {
                throw new Exception("Day1. Tests failed. Did not run GetOutput.");
            }

        }

        /// <summary>
        /// Do the things
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static int GetOutput(string[] input)
        {
            int output = 0;
            
            foreach(string line in input)
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
                    throw new Exception($"Day1. Error has occurred during calculation due to line {tempLine}");
                }
            }

            return output;
        }

        

        /// <summary>
        /// Test Day 1 algorithm against known values
        /// </summary>
        /// <returns></returns>
        public static bool Validate()
        {
            string[] testArray1 = { "+1", "+1", "+1" };
            int testResult = GetOutput(testArray1);
            if(testResult != 3)
            {
                return false;
            }

            string[] testArray2 = { "+1", "+1", "-2" };
            testResult = GetOutput(testArray2);
            if (testResult != 0)
            {
                return false;
            }

            string[] testArray3 = { "-1", "-2", "-3" };
            testResult = GetOutput(testArray3);
            if (testResult != -6)
            {
                return false;
            }

            return true;

        }
    }
}
