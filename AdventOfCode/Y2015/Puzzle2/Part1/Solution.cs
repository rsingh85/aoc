using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2015.Puzzle2.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var dimensions = File.ReadAllLines(Helper.GetInputFilePath(this));
            var presents = dimensions.Select(d => new Present(d));

            var totalSquareFeet = presents.Sum(p => p.GetRequiredSurfaceArea());

            Console.WriteLine(totalSquareFeet);
        }
    }

    public class Present
    {
        public int Length { get; private set; }
        public int Width { get; private  set; }
        public int Height { get; private set; }

        public Present(string dimensions)
        {
            var splitDimensions = dimensions.Split('x');

            Length = Convert.ToInt32(splitDimensions[0]);
            Width = Convert.ToInt32(splitDimensions[1]);
            Height = Convert.ToInt32(splitDimensions[2]);
        }

        public int GetRequiredSurfaceArea()
        {
            var a = Length * Width;
            var b = Width * Height;
            var c = Height * Length;
            var slack = new List<int> { a, b, c }.Min();

            return (2 * (a + b + c)) + slack;
        }
    }
}