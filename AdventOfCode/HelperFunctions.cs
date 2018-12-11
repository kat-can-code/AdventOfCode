using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    static class HelperFunctions
    {
        /// <summary>
        /// Read file from path and return as string array
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> ReadFile(string filePath)
        {
            try
            {
                string[] fileContents = System.IO.File.ReadAllLines(filePath);
                List<string> fileContentsList = new List<string>(fileContents);
                return fileContentsList;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Unable to read file at {filePath} " + e.Message);
                return null;
            }
        }

        public static void SafeSolve(string input)
        {
            try
            {
                if (ValidateInput(input))
                {
                    Console.WriteLine($"Solving day {SafeGetDay(input)}, part {SafeGetPart(input)}");
                    switch (input)
                    {
                        case "1.1":
                            Day1.Solve("Data/D1P1.txt");
                            break;
                        case "1.2":
                            Day1.Solve("Data/D1P1.txt", true);
                            break;
                        case "2.1":
                            Day2.Solve("Data/D2P1.txt");
                            break;
                        case "2.2":
                            Day2.Solve("Data/D2P1.txt", true);
                            break;
                        case "3.1":
                            Day3.Solve("Data/D3P1.txt", true);
                            break;
                        case "4.1":
                            Day4.Solve("Data/D4P1.txt");
                            break;
                        case "4.2":
                            Day4.Solve("Data/D4P1.txt", true);
                            break;
                        case "5.1":
                            Day5.Solve("Data/D5P1.txt");
                            break;
                        case "5.2":
                            Day5.Solve("Data/D5P1.txt", true);
                            break;
                        default:
                            Console.WriteLine("No puzzle found for specified date. Try again.");
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"We done goofed on day {SafeGetDay(input)}. " + e.Message);
            }
        }

        private static bool ValidateInput(string input)
        {
            if (input.Length > 4)
            {
                Console.WriteLine("Input length is too long.");
                return false;
            }
            else if ((input.Length == 1 && !String.Equals(input, "e", StringComparison.OrdinalIgnoreCase)) || !input.Contains("."))
            {
                Console.WriteLine("Please enter valid input in the form of 1.1, 1.2, etc");
                return false;
            }
            else if(SafeGetDay(input) < 0)
            {
                Console.WriteLine("Please enter a valid day.");
                return false;
            }
            else if(SafeGetPart(input) < 0)
            {
                Console.WriteLine("Please enter part 1 or 2.");
                return false;
            }
            return true;
        }

        private static int SafeGetDay(string input)
        {
            try {
                int result = Convert.ToInt32(input.Split('.')[0]);
                if(result <1 || result > 24)
                {
                    Console.WriteLine("Input date is out of range");
                    return -1;
                }
                return result;
            }
            catch
            {
                return -1;
            }
        }

        private static int SafeGetPart(string input)
        {
            try
            {
                int result = Convert.ToInt32(input.Split('.')[1]);
                if(result !=1 && result != 2)
                {
                    Console.WriteLine("Input part is out of range");
                    return -1;
                }
                return result;
            }
            catch
            {
                return -1;
            }
        }
    }
}
