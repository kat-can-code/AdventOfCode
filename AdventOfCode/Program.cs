using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter the day and part in the form of 1.1, 1.2, 2.1...etc");
                string input = Console.ReadLine();
                input = input.ToLower();
                while (!String.Equals(input, "e", StringComparison.OrdinalIgnoreCase))
                {
                    HelperFunctions.SafeSolve(input);

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
