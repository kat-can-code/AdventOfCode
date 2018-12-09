using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day2
    {
        public static void Solve(string inputPath, bool isP2 = false)
        {
            if (Validate())
            {
                List<string> input = HelperFunctions.ReadFile(inputPath);
                if (isP2)
                {
                    string p2Result = GetAlmostMatchString(input);
                    Console.WriteLine($"Solution is {p2Result}");
                }
                else
                {
                    int result = GetChecksum(input);
                    Console.WriteLine($"Solution is {result}");
                }
            }
            else
            {
                Console.WriteLine("Tests failed. Did not run day 2.");
            }
        }

        /// <summary>
        /// Get frequency Dictionary for the phrase token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Dictionary has (letter, frequency) key value pairs</returns>
        private static Dictionary<char, int> ParseLetters(string token)
        {
            Dictionary<char, int> result = new Dictionary<char, int>();

            for(int i = 0; i < token.Length; i++)
            {
                if (!result.Keys.Contains(token[i]))
                {
                    result.Add(token[i], 1);
                }
                else
                {
                    result[token[i]]++;
                }
            }

            return result;
        }

        /// <summary>
        /// Return checksum
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static int GetChecksum(List<string> input)
        {
            int twoLetterCount  = 0;
            int threeLetterCount = 0;

            foreach(string token in input)
            {
                Dictionary<char, int> tokenDictionary = ParseLetters(token);
                if (ContainsTwoLetters(tokenDictionary))
                {
                    twoLetterCount++;
                }
                if (ContainsThreeLetters(tokenDictionary))
                {
                    threeLetterCount++;
                }
            }
            int result = twoLetterCount * threeLetterCount;

            return result;
        }

        /// <summary>
        /// Helper to check if two letter occurrences exist
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool ContainsTwoLetters(Dictionary<char, int> input)
        {
            if(input.Where(p => p.Value == 2).Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Helper to check if three letter occurrences exist
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool ContainsThreeLetters(Dictionary<char, int> input)
        {
            if (input.Where(p => p.Value == 3).Count() > 0)
            {
                return true;
            }

            return false;
        }

        private static string GetAlmostMatchString(List<string> input)
        {
            List<string> processedList = new List<string>();
            foreach(string inputRow in input)
            {
                if (processedList.Count == 0)
                {
                    processedList.Add(inputRow);
                }
                else
                {
                    foreach(string processedRow in processedList)
                    {
                        var result = GetStringDistance(inputRow, processedRow);
                        if (result.Item1 == 1)
                        {
                            return result.Item2;
                        }
                    }
                    processedList.Add(inputRow);
                }
            }
            return String.Empty;
        }

        private static Tuple<int, string> GetStringDistance(string input1, string input2)
        {
            StringBuilder matches = new StringBuilder();
            int distance = 0;
            
            if(input1.Length == input2.Length)
            {
                for (int i = 0; i < input1.Length; i++)
                {
                    if (input1[i] != input2[i])
                    {
                        distance++;
                    }
                    else
                    {
                        matches.Append(input1[i]);
                    }
                }
            }

            return new Tuple<int, string>(distance, matches.ToString());
        }

        public static bool Validate()
        {
            List<string> testList = new List<string>() { "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab"};
            int result = GetChecksum(testList);
            if(result != 12)
            {
                return false;
            }

            testList = new List<string>() { "abcde", "fghij", "klmno", "pqrst", "fguij", "axcye", "wvxyz" };
            string p2Result = GetAlmostMatchString(testList);
            if (!String.Equals(p2Result, "fgij", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
        }
    }
}
