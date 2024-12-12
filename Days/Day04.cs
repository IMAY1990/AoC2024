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
            int testcounter1 = 0;
            for (int j = 0; j < input.Rows.Count(); j++)
            {
                for (int i = 0; i < input.Rows[j].Length; i++)
                {
                    
                    if (input.Rows[j][i] == 'X')
                    {
                        testcounter1++;
                        foreach (var vector in vectorList)
                        {
                            int toAdd = CheckInputVsVector(vector, i, j);
                            xmasCounter += toAdd;
                            if (toAdd == 1)
                            {
                                Console.WriteLine($"Upped counter for X nr. {testcounter1}, position {i-2},{j-2}. Direction {vector}");
                            }
                            
                        }

                    }
                }

            }
            Console.WriteLine(xmasCounter);
        }

        public override void DoPart2()
        {

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
    }
}
