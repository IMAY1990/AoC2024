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
            GetStepsToExit();
        }

        public override void DoPart2()
        {
            for (int i = 1024; i < input.Rows.Count; i++)
            {
                Point toCorrupt = GetCoords(input.Rows[i], 1);
                AddCorruption(toCorrupt);

                if (!GetStepsToExit())
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
        private bool GetStepsToExit()
        {
            Dictionary<(int X, int Y), int> reachableInSteps = new Dictionary<(int X, int Y), int>();
            int startX = 0; int startY = 0;
            (startX, startY) = GetStart(kaart);
            int endX = 0; int endY = 0;
            (endX, endY) = GetEnd(kaart);

            reachableInSteps.Add((startX, startY), 0);
            bool foundEnd = false;
            bool addedCoords = true;
            int steps = 0;
            while (!foundEnd && addedCoords)
            {
                addedCoords = false;
                List<(int X, int Y)> nextReachable = new List<(int X, int Y)>();
                foreach (var current in reachableInSteps.Where(x => x.Value == steps))
                {
                    List<(int X, int Y)> reachableList = GetSurroundingReachable(current.Key, kaart);
                    nextReachable.AddRange(reachableList);

                }

                foreach (var reachable in nextReachable)
                {
                    if (!reachableInSteps.ContainsKey(reachable))
                    {
                        addedCoords = true;
                        reachableInSteps.Add(reachable, steps + 1);
                    }
                }

                if (reachableInSteps.ContainsKey((endX, endY)))
                {
                    foundEnd = true;
                    Console.WriteLine($"End reached in {reachableInSteps[(endX, endY)]} steps");
                }
                steps++;
            }

            Console.WriteLine($"End found: {foundEnd}");

            return foundEnd;
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

        (int sx, int sy) GetStart(List<string> kaart)
        {
            int sx = 0, sy = 0;
            for (int j = 0; j < kaart.Count; j++)
            {
                var x = kaart[j].IndexOf('S');
                if (x != -1)
                {
                    (sx, sy) = (x, j);
                    break;
                }
            }
            return (sx, sy);
        }

        (int sx, int sy) GetEnd(List<string> kaart)
        {
            int sx = 0, sy = 0;
            for (int j = 0; j < kaart.Count; j++)
            {
                var x = kaart[j].IndexOf('E');
                if (x != -1)
                {
                    (sx, sy) = (x, j);
                    break;
                }
            }
            return (sx, sy);
        }

        List<(int X, int Y)> GetSurroundingReachable((int X, int Y) current, List<string> kaart)
        {
            List<(int X, int Y)> surroundingReachable = new List<(int X, int Y)> ();
            if (kaart[current.Y][current.X - 1] == '.' || kaart[current.Y][current.X - 1] == 'E')
            {
                surroundingReachable.Add((current.X - 1, current.Y));
            }
            if (kaart[current.Y][current.X + 1] == '.' || kaart[current.Y][current.X + 1] == 'E')
            {
                surroundingReachable.Add((current.X + 1, current.Y));
            }
            if (kaart[current.Y - 1][current.X] == '.' || kaart[current.Y - 1][current.X] == 'E')
            {
                surroundingReachable.Add((current.X, current.Y - 1));
            }
            if (kaart[current.Y + 1][current.X] == '.' || kaart[current.Y + 1][current.X] == 'E')
            {
                surroundingReachable.Add((current.X, current.Y + 1));
            }

            return surroundingReachable;
        }
    }
}
