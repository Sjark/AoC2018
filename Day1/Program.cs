using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1a
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = File.ReadAllLines("Input\\input.txt")
                .Select(a => int.Parse(a))
                .ToList();

            var freq = 0;

            for (int i = 0; i < file.Count; i++)
            {
                freq += file[i];
            }

            Console.WriteLine($"Day 1a: {freq}");

            var prevFreqs = new HashSet<int>();
            var y = 0;
            var currentFreq = 0;

            while (!prevFreqs.Contains(currentFreq))
            {
                prevFreqs.Add(currentFreq);
                if (y == file.Count)
                {
                    y = 0;
                }

                currentFreq += file[y];
                y++;
            }

            Console.WriteLine($"Day 1b: {currentFreq}");
            Console.Read();
        }
    }
}
