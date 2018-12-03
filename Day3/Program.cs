using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = File.ReadAllLines("Input\\input.txt");
            
            var fabric = new string[1000, 1000];
            foreach (var line in file)
            {
                var id = line.Split('@')[0].Trim().Replace("#", "");
                var cords = line.Split('@')[1].Split(':')[0].Split(',')
                    .Select(x => int.Parse(x))
                    .ToArray();

                var size = line.Split('@')[1].Split(':')[1].Trim().Split('x')
                    .Select(x => int.Parse(x))
                    .ToArray();

                for (int i = 0; i < size[0]; i++)
                {
                    for (int y = 0; y < size[1]; y++)
                    {
                        if (fabric[i + cords[0], y + cords[1]] == null)
                        {
                            fabric[i + cords[0], y + cords[1]] = id;
                        }
                        else
                        {
                            fabric[i + cords[0], y + cords[1]] = "X";
                        }
                    }
                }
            }

            var overlappingSquares = 0;

            for (int i = 0; i < fabric.GetLength(0); i++)
            {
                for (int j = 0; j < fabric.GetLength(1); j++)
                {
                    var s = fabric[i, j];

                    if (s == "X")
                    {
                        overlappingSquares++;
                    }
                }
            }
            
            Console.WriteLine($"Day 3a: {overlappingSquares}");

            foreach (var line in file)
            {
                var id = line.Split('@')[0].Trim().Replace("#", "");
                var cords = line.Split('@')[1].Split(':')[0].Split(',')
                    .Select(x => int.Parse(x))
                    .ToArray();

                var size = line.Split('@')[1].Split(':')[1].Trim().Split('x')
                    .Select(x => int.Parse(x))
                    .ToArray();

                var isOverlapping = false;

                for (int i = 0; i < size[0]; i++)
                {
                    for (int y = 0; y < size[1]; y++)
                    {
                        if (fabric[i + cords[0], y + cords[1]] == "X")
                        {
                            isOverlapping = true;
                            break;
                        }
                    }

                    if (isOverlapping)
                    {
                        break;
                    }
                }

                if (!isOverlapping)
                {
                    Console.WriteLine($"Day 3b: {id}");
                    break;
                }
            }
            
            Console.Read();
        }
    }
}
