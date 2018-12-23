using System;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input\\input.txt")[0];

            Console.WriteLine($"Day 5a: {ReactPolymerCount(input)}");

            var uniqueChars = input.Select(a => char.ToLower(a))
                .Distinct();

            var shortestPolymer = int.MaxValue;

            foreach (var c in uniqueChars)
            {
                var count = ReactPolymerCount(CleanString(input, c));

                if (count < shortestPolymer)
                {
                    shortestPolymer = count;
                }
            }

            Console.WriteLine($"Day 5b: {shortestPolymer}");

            Console.Read();
        }

        private static int ReactPolymerCount(string input)
        {
            var inputLength = input.Length - 1;
            for (int i = 0; i < inputLength; i++)
            {
                if (i < 0)
                {
                    i = 0;
                }

                if (i + 1 < input.Length && char.ToUpper(input[i]) == char.ToUpper(input[i + 1]) && input[i] != input[i + 1])
                {
                    input = input.Remove(i, 2);
                    i -= 2;
                }
            }

            return input.Length;
        }

        private static string CleanString(string s, char c)
        {
            int len = s.Length;
            char[] s2 = new char[len];
            int i2 = 0;
            for (int i = 0; i < len; i++)
            {
                char x = s[i];
                if (x != c && x != char.ToUpper(c))
                    s2[i2++] = x;
            }
            return new string(s2, 0, i2);
        }
    }
}
