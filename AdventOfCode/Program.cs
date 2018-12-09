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
                input = input.ToLower();
                while (!String.Equals(input, "e", StringComparison.OrdinalIgnoreCase))
                {
                    switch (input)
                    {
                        case "d1p1":
                            Day1.Solve("Data/D1P1.txt");
                            break;
                        case "d1p2":
                            Day1.Solve("Data/D1P1.txt", true);
                            break;
                        default:
                            Console.WriteLine("Please enter a valid selection.");
                            break;
                    }

                    Console.WriteLine("Type e to exit. Or select another puzzle in the format D1P1, D1P2 etc.");
                    input = Console.ReadLine().ToLower();
                }    
            }
            catch(Exception e)
            {
                Console.WriteLine("Error occurred during processing.", e.Message);
            }
        }
    }
}
