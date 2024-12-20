using AoC2024.Generic;
using AoC2024.Generic.HelperClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day18 : Day
    {
        List<string> kaart = new List<string>();

        public Day18(int day) : base(day)
        {
        }

        public override void DoPart1()
        {
            GetKaart();
            Maps.GetStepsToExit(kaart);
        }

        public override void DoPart2()
        {
            for (int i = 1024; i < input.Rows.Count; i++)
            {
                Point toCorrupt = GetCoords(input.Rows[i], 1);
                AddCorruption(toCorrupt);

                if (!Maps.GetStepsToExit(kaart).foundEnd)
                {
                    Console.WriteLine($"One corruption too many at {toCorrupt.X - 1},{toCorrupt.Y - 1}");
                    break;
                }
            }
        }

        private void GetKaart()
        {
            for (int i = 0; i < 71; i++)
            {
                kaart.Add(new string('.', 71));
            }

            for (int i = 0; i < 1024; i++)
            {
                Point toCorrupt = GetCoords(input.Rows[i]);
                AddCorruption(toCorrupt);
            }
            kaart[0] = GetNewRow(kaart[0], 0, 'S');
            kaart[kaart.Count - 1] = GetNewRow(kaart[kaart.Count - 1], kaart[kaart.Count - 1].Length - 1, 'E');
            kaart.Insert(0, new string('#', 71));
            kaart.Add(new string('#', 71));
            for (int i = 0; i < kaart.Count; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append('#');
                sb.Append(kaart[i]);
                sb.Append('#');

                kaart[i] = sb.ToString();
            }
            for (int i = 0; i < kaart.Count; i++)
            {
                Console.WriteLine(kaart[i]);
            }
        }

        private void AddCorruption(Point toCorrupt)
        {
            kaart[toCorrupt.Y] = GetNewRow(kaart[toCorrupt.Y], toCorrupt.X, '#');
        }

        Point GetCoords(string inputRow, int offset = 0)
        {
            string[] splitCoords = inputRow.Split(',');
            return new Point(Convert.ToInt32(splitCoords[0]) + offset, Convert.ToInt32(splitCoords[1]) + offset);
        }

        string GetNewRow(string startRow, int index, char newChar)
        {
            return new StringBuilder(startRow) { [index] = newChar }.ToString();
        }

    }
}
