using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day4 : Day
    {
        List<Vector2> vectorList = new List<Vector2>() {
            new Vector2(0, 1), //N
            new Vector2(1, 1), //NE
            new Vector2(1, 0), //E
            new Vector2(1, -1), //SE
            new Vector2(0, -1), //S
            new Vector2(-1, -1), //SW
            new Vector2(-1, 0), //W
            new Vector2(-1, 1)}; //NW

        public Day4(int day) : base(day)
        {
        }

        public override void DoPart1()
        {
            int xmasCounter = 0;
            input.Rows = input.AddPadding(3,'.');
            for (int j = 0; j < input.Rows.Count(); j++)
            {
                for (int i = 0; i < input.Rows[j].Length; i++)
                {
                    
                    if (input.Rows[j][i] == 'X')
                    {
                        foreach (var vector in vectorList)
                        {
                            int toAdd = CheckInputVsVector(vector, i, j);
                            xmasCounter += toAdd;                            
                        }

                    }
                }

            }
            Console.WriteLine(xmasCounter);
        }

        public override void DoPart2()
        {
            int xmasCounter = 0;
            for (int j = 0; j < input.Rows.Count(); j++)
            {
                for (int i = 0; i < input.Rows[j].Length; i++)
                {
                    if (input.Rows[j][i] == 'A')
                    {
                        xmasCounter += CheckforXmas((i, j));
                    }
                }
            }

            Console.WriteLine(xmasCounter);
        }

       int CheckInputVsVector(Vector2 vector, int beginX, int beginY)
        {
            int horizontal = Convert.ToInt32(vector[0]);
            int vertical = Convert.ToInt32(vector[1]);
            if (input.Rows[beginY + vertical][beginX + horizontal] != 'M')
                return 0;
            if (input.Rows[beginY + (2 * vertical)][beginX + (2 * horizontal)] != 'A')
                return 0;
            if (input.Rows[beginY + (3 * vertical)][beginX + (3 * horizontal)] != 'S')
                return 0;
            return 1;
        }

        int CheckforXmas((int X, int Y) aPositie)
        {
            int nrOfXmas = 0;
            char tocheck1 = input.Rows[aPositie.Y + 1][aPositie.X - 1];
            char tocheck2 = input.Rows[aPositie.Y - 1][aPositie.X + 1];
            if (tocheck1 == 'M' && tocheck2 == 'S' || tocheck1 == 'S' && tocheck2 == 'M')
            {
                char tocheck3 = input.Rows[aPositie.Y - 1][aPositie.X - 1];
                char tocheck4 = input.Rows[aPositie.Y + 1][aPositie.X + 1];

                if (tocheck3 == 'M' && tocheck4 == 'S' || tocheck3 == 'S' && tocheck4 == 'M')
                {
                    nrOfXmas++;
                }
            }

            return nrOfXmas;
        }
    }
}
