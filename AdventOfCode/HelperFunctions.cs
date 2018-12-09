﻿using System;
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
        public static string[] ReadFile(string filePath)
        {
            try
            {
                string[] fileContents = System.IO.File.ReadAllLines(filePath);
                return fileContents;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Unable to read file at {filePath}");
                return null;
            }
        }
    }
}
