using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter the day and problem to solve, in the format of D1P1, D1P2...etc");
                string input = Console.ReadLine();
                while (input != "E")
                {
                    switch (input)
                    {
                        case "D1P1":
                            Console.WriteLine("Day 1 Solution: " + Day1.Solve("Data/D1P1.txt"));
                            break;
                        default:
                            Console.WriteLine("Please enter a valid selection.");
                            break;
                    }

                    Console.WriteLine("Type E to exit. Or select another puzzle in the format D1P1, D1P2 etc.");
                    input = Console.ReadLine();
                }    
            }
            catch(Exception e)
            {
                Console.WriteLine("Error occurred during processing.", e.Message);
            }
        }
    }
}
