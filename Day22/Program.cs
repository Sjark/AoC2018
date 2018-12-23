using System;
using System.Drawing;
using System.Linq;

namespace Day22
{
    class Program
    {
        static void Main(string[] args)
        {
            var depth = 8103;
            var target = new Point(9, 758);

            var cave = CreateCave(target.X, target.Y, depth);

            var riskLevel = 0;

            for (int k = 0; k < cave.GetLength(0); k++)
            {
                for (int l = 0; l < cave.GetLength(1); l++)
                {
                    riskLevel += (int)cave[k, l].CaveType;
                    switch (cave[k, l].CaveType)
                    {
                        case CaveType.Rocky:
                            Console.Write('.');
                            break;
                        case CaveType.Wet:
                            Console.Write('=');
                            break;
                        case CaveType.Narrow:
                            Console.Write('|');
                            break;
                        default:
                            break;
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine(riskLevel);

            Console.Read();
        }

        static CaveRegion[,] CreateCave(int x, int y, int depth) {
            var cave = new CaveRegion[x + 1, y + 1];

            for (int xcoord = 0; xcoord <= x; xcoord++)
            {
                for (int ycoord = 0; ycoord <= y; ycoord++)
                {
                    int geologicIndex;

                    if ((xcoord == 0 && ycoord == 0) || (xcoord == x && ycoord == y))
                    {
                        geologicIndex = 0;
                    }
                    else if (ycoord == 0)
                    {
                        geologicIndex = xcoord * 16807;
                    }
                    else if (xcoord == 0)
                    {
                        geologicIndex = ycoord * 48271;
                    }
                    else
                    {
                        geologicIndex = cave[xcoord - 1, ycoord].ErosinLevel * cave[xcoord, ycoord - 1].ErosinLevel;
                    }

                    var erosinLevel = (geologicIndex + depth) % 20183;
                    var caveType = (CaveType)(erosinLevel % 3);
                    cave[xcoord, ycoord] = new CaveRegion(caveType, erosinLevel, geologicIndex);
                }
            }

            return cave;
        }
    }

    public class CaveRegion
    {
        public CaveRegion(CaveType caveType, int erosinLevel, int geologicIndex)
        {
            CaveType = caveType;
            ErosinLevel = erosinLevel;
            GeologicIndex = geologicIndex;
        }

        public CaveType CaveType { get; }
        public int ErosinLevel { get; }
        public int GeologicIndex { get; }
    }

    public enum CaveType
    {
        Rocky = 0,
        Wet = 1,
        Narrow = 2
    }
}
