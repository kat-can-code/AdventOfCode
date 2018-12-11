using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;

namespace AdventOfCode
{
    class Day3
    {
        public static void Solve(string inputPath, bool isP2 = false)
        {
            if(Validate())
            {

                List<string> input = HelperFunctions.ReadFile(inputPath);
                List<Claim> claims = ParseIntoClaims(input);
            }
            else
            {

            }
        }

        private static bool DetectCollision(Claim claim1, Claim claim2)
        {
            Rectangle intersect = Rectangle.Intersect(claim1.rect, claim2.rect);
            if (intersect == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Input rows are of format #1 @ 1,3: 4x4
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static List<Claim> ParseIntoClaims(List<string> input)
        {
            List<Claim> claims = new List<Claim>();
            foreach(string inputRow in input)
            {
                Regex g = new Regex("#([0-9]+) @ ([0-9]+)[,]([0-9]+)[:] ([0-9]+)x([0-9]+)");
                var m = g.Match(inputRow);
                if (m.Success == true)
                {
                    Claim claim = new Claim(Convert.ToInt32(m.Groups[1].Value), Convert.ToInt32(m.Groups[2].Value), Convert.ToInt32(m.Groups[3].Value), Convert.ToInt32(m.Groups[4].Value), Convert.ToInt32(m.Groups[5].Value));
                    claims.Add(claim);
                }
                else
                {
                    throw new Exception("Input row did not match regex for Claim format.");
                }
            }

            return claims;
        }

        public static bool Validate()
        {
            List<string> testInput = new List<string>(){ "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4" };
            var claims = ParseIntoClaims(testInput);
            DetectCollision(claims[0], claims[1]);

            return true;
        }
    }

    class Claim
    {
        public int ClaimId;
        public int xCoord;
        public int yCoord;
        public int xRange;
        public int yRange;
        public Rectangle rect;

        public Claim(int claimId, int xCoord, int yCoord, int xRange, int yRange)
        {
            this.ClaimId = claimId;
            this.xCoord = xCoord;
            this.xRange = xRange;
            this.yCoord = yCoord;
            this.yRange = yRange;
            rect = new Rectangle(xCoord, yCoord, xRange, yRange);
        }
    }
}
