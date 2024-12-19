using AoC2024.Generic.HelperClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day6 : Day
    {
        char[,] inputGridP1;
        MovingObject guard;
        bool leftGrid = false;
        int width = 0, height = 0;
        List<(int X, int Y)> visitedPositions = new List<(int X, int Y)>();

        public Day6(int day) : base(day)
        {
            inputGridP1 = input.GetCharGrid();
        }

        public override void DoPart1()
        {
            width = inputGridP1.GetLength(0);
            height = inputGridP1.GetLength(1);
            guard = GetStart(inputGridP1);

            while (!leftGrid)
            {
                TakeNextStep(inputGridP1);
                if (!visitedPositions.Contains(guard.position))
                {
                    visitedPositions.Add(guard.position);
                }
                
            }

            int xCounter = 0;
            foreach (var item in inputGridP1)
            {
                if (item == 'X')
                {
                    xCounter++;
                }
            }
            Console.WriteLine(xCounter);
            Console.WriteLine(visitedPositions.Count);
            //DrawMap();
        }

        public override void DoPart2()
        {            
            int options = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            options = 0;
            int counter = 0;
            foreach (var position in visitedPositions)
            {
                counter++;
                //Console.WriteLine(counter);
                char[,] inputGridP2 = input.GetCharGrid();
                guard = GetStart(inputGridP2);
                //Console.WriteLine($"Blocking: ({position.X},{position.Y})");
                Dictionary<(int X, int Y), List<WindDirection>> visitedInDirections = new Dictionary<(int X, int Y), List<WindDirection>>();
                leftGrid = false;

                if (guard.position == position)
                    continue;
                if (inputGridP2[position.X, position.Y] == '#')
                    continue;

                inputGridP2[position.X, position.Y] = '#';
                bool alreadyVisitedInDirection = 
                    visitedInDirections.ContainsKey(guard.position) ?
                    visitedInDirections[guard.position].Contains(guard.direction.windDirection) : 
                    false;

                while (!leftGrid && !alreadyVisitedInDirection)
                {
                    if (visitedInDirections.ContainsKey(guard.position))
                        if (visitedInDirections[guard.position].Contains(guard.direction.windDirection))
                            alreadyVisitedInDirection = true;
                        else
                            visitedInDirections[guard.position].Add(guard.direction.windDirection);
                    else
                        visitedInDirections.Add(guard.position, new List<WindDirection>() { guard.direction.windDirection });
                    TakeNextStep(inputGridP2);
                }

                
                //DrawMap(inputGridP2);
                //Console.WriteLine();

                if (!leftGrid)
                {
                    options++;
                }
            }
            Console.WriteLine($"Part 2: {options} out of {counter} tried");
        }
        
        private MovingObject GetStart(char[,] inputGrid)
        {
            MovingObject newGuard = new MovingObject();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (inputGrid[x,y] == '^')
                    {
                        return new MovingObject() { direction = new Direction(WindDirection.N, (0, -1)), position = (x,y) };
                    }
                    if (inputGrid[x, y] == '>')
                    {
                        return new MovingObject() { direction = new Direction(WindDirection.E, (1, 0)), position = (x, y) };
                    }
                    if (inputGrid[x, y] == 'v')
                    {
                        return new MovingObject() { direction = new Direction(WindDirection.S, (0, 1)), position = (x, y) };
                    }
                    if (inputGrid[x, y] == '<')
                    {
                        return new MovingObject() { direction = new Direction(WindDirection.W, (-1, 0)), position = (x, y) };
                    }
                }
            }

            return newGuard;
        }
        private void TakeNextStep(char[,] grid)
        {
            //Mark current tile
            grid[guard.position.X, guard.position.Y] = 'X';

            //check if leftGrid
            if (guard.position.X + guard.direction.direction.X < 0 || 
                guard.position.X + guard.direction.direction.X > width - 1 ||
                guard.position.Y + guard.direction.direction.Y < 0 ||
                guard.position.Y + guard.direction.direction.Y > height - 1)
            {
                leftGrid = true;
                return;
            }

            //Check if next tile is wall
            if (grid[guard.position.X + guard.direction.direction.X, guard.position.Y + guard.direction.direction.Y] == '#')
            {
                guard.direction = guard.GetNextDirection();
                return;
            }

            //Step forward
            guard.position = (guard.position.X + guard.direction.direction.X, guard.position.Y + guard.direction.direction.Y);
            return;
        }

        private void DrawMap(char[,] grid)
        {
            for (int j = 0; j < height; j++)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < width; i++)
                {
                    sb.Append(grid[i, j]);
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
