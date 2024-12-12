using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day10 : Day
    {
        List<Point> stepDirections = new List<Point>()
        {
            new Point(0,1),
            new Point(1,0),
            new Point(-1,0),
            new Point(0,-1)
        };

        int[,] inputGrid;
        public Day10(int day) : base(day)
        {
            inputGrid = input.GetIntGrid();
        }

        public override void DoPart1()
        {
            //Find all trailheads
            List<Point> trailheads = GetTrailheads();

            int total = 0;
            foreach (var trailhead in trailheads)
            {
                List<Point> possibleSpots = GetTrailheadValue(trailhead);
                total += possibleSpots.Distinct().Count();
            }
            Console.WriteLine(total);
        }
        public override void DoPart2()
        {
            List<Point> trailheads = GetTrailheads();

            int total = 0;
            foreach (var trailhead in trailheads)
            {
                List<Point> possibleSpots = GetTrailheadValue(trailhead);
                total += possibleSpots.Count();
            }
            Console.WriteLine(total);
        }

        private List<Point> GetTrailheadValue(Point trailhead)
        {
            List<Point> possibleSpots = new List<Point>();
            possibleSpots.Add(trailhead);


            for (int i = 1; i < 10; i++)
            {
                List<Point> possibleNextSteps = new List<Point>();
                foreach (var point in possibleSpots)
                {
                    possibleNextSteps.AddRange(GetNextPoints(point));
                }
                possibleSpots = new List<Point>();
                possibleSpots.AddRange(possibleNextSteps);

            }

            return possibleSpots;
        }

        private List<Point> GetTrailheads()
        {
            List<Point> trailheads = new List<Point>();
            for (int j = 0; j < inputGrid.GetLength(1); j++)
            {
                for (int i = 0; i < inputGrid.GetLength(0); i++)
                {
                    if (inputGrid[i, j] == 0)
                    {
                        trailheads.Add(new Point(i, j));
                    }
                }
            }

            return trailheads;
        }

        List<Point> GetNextPoints(Point point)
        {
            List<Point> nextPoints = new List<Point>();
            int height = inputGrid[point.X, point.Y];
            foreach (Point p in stepDirections)
            {
                try
                {
                    if (inputGrid[point.X + p.X, point.Y + p.Y] == height + 1)
                    {
                        nextPoints.Add(new Point(point.X + p.X, point.Y + p.Y));
                    }
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            return nextPoints;
        }


    }
}
