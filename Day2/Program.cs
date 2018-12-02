using System;
using System.Collections.Generic;
using System.IO;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = File.ReadAllLines("Input\\input.txt");

            var twos = 0;
            var threes = 0;

            foreach (var line in file)
            {
                var resultDict = new Dictionary<char, int>();
                foreach (var letter in line)
                {
                    if (resultDict.ContainsKey(letter))
                    {
                        resultDict[letter]++;
                    }
                    else
                    {
                        resultDict.Add(letter, 1);
                    }
                }

                var twosFound = false;
                var threesFound = false;
                foreach (var key in resultDict.Keys)
                {
                    if (!twosFound && resultDict[key] == 2)
                    {
                        twosFound = true;
                        twos++;
                    }
                    if (!threesFound && resultDict[key] == 3)
                    {
                        threesFound = true;
                        threes++;
                    }
                    if (threesFound && twosFound)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine($"Day 2a: {twos * threes}");

            for (int i = 0; i < file.Length; i++)
            {
                var wordToCompare = file[i];

                for (int y = i + 1; y < file.Length; y++)
                {
                    var word = file[y];
                    var charsOff = 0;
                    var charOffPos = -1;

                    for (int z = 0; z < wordToCompare.Length; z++)
                    {
                        if (wordToCompare[z] != word[z])
                        {
                            charsOff++;
                            charOffPos = z;
                        }

                        if (charsOff > 1)
                        {
                            break;
                        }
                    }

                    if (charsOff == 1)
                    {
                        Console.WriteLine($"Day 2b: {word.Remove(charOffPos, 1)}");
                    }
                }
            }

            Console.Read();
        }
    }
}
