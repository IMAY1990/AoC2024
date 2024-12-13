using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day12 : Day
    {
        List<(long area, long perimeter, long sides)> regions = new List<(long area, long perimeter, long sides)>();
        public Day12(int day) : base(day)
        {
        }

        public override void DoPart1()
        {
            GetAllRegions();
            long answer = 0;
            foreach (var region in regions)
            {
                answer += region.area * region.perimeter;
            }

            Console.WriteLine(answer);
        }

        public override void DoPart2()
        {
            long answer = 0;
            foreach (var region in regions)
            {
                answer += region.area * region.sides;
            }

            Console.WriteLine(answer);
        }

        void GetAllRegions()
        {
            bool[,] visited = new bool[input.Rows[0].Length, input.Rows.Count];

            Queue<Point> work = new Queue<Point>();

            int currentRegion = 0;
            for (int y = 0; y < input.Rows.Count; y++)
            {
                for (int x = 0; x < input.Rows[y].Length; x++)
                {
                    if (!visited[x, y])
                    {
                        work.Enqueue(new Point(x, y));

                        HashSet<(int x, int y, int direction)> fence = new HashSet<(int x, int y, int direction)>();
                        char planttype = input.Rows[y][x];
                        regions.Add((0, 0, 0));
                        while (work.Count > 0)
                        {
                            Point cur = work.Dequeue();
                            if (!visited[cur.X, cur.Y])
                            {
                                visited[cur.X, cur.Y] = true;

                                int dowork(int x1, int y1, int direction)
                                {
                                    if (y1 >= 0 && x1 >= 0 && y1 < input.Rows.Count && x1 < input.Rows[y1].Length)
                                    {
                                        if (input.Rows[y1][x1] == planttype)
                                        {
                                            if (!visited[x1, y1]) work.Enqueue(new Point(x1, y1));
                                            return 0;
                                        }

                                    }

                                    fence.Add((x1, y1, direction));
                                    return 1;
                                }

                                int perimeter = dowork(cur.X, cur.Y - 1, 1);
                                perimeter += dowork(cur.X, cur.Y + 1, 2);
                                perimeter += dowork(cur.X - 1, cur.Y, 3);
                                perimeter += dowork(cur.X + 1, cur.Y, 4);
                                regions[currentRegion] = (regions[currentRegion].area + 1, regions[currentRegion].perimeter + perimeter, 0);
                            }

                        }
                        int sides = 0;
                        while (fence.Count > 0)
                        {
                            sides++;
                            (int x, int y, int direction) cur = fence.First();
                            fence.Remove(cur);
                            (int x1, int y1, int d1) = cur;
                            while (fence.Contains((x1 + 1, y1, d1))) { fence.Remove((x1 + 1, y1, d1)); x1++; }
                            (x1, y1, d1) = cur;
                            while (fence.Contains((x1 - 1, y1, d1))) { fence.Remove((x1 - 1, y1, d1)); x1--; }
                            (x1, y1, d1) = cur;
                            while (fence.Contains((x1, y1 + 1, d1))) { fence.Remove((x1, y1 + 1, d1)); y1++; }
                            (x1, y1, d1) = cur;
                            while (fence.Contains((x1, y1 - 1, d1))) { fence.Remove((x1, y1 - 1, d1)); y1--; }

                        }
                        regions[currentRegion] = (regions[currentRegion].area, regions[currentRegion].perimeter, sides);
                        currentRegion++;
                    }
                }
            }
        }
    }
}
