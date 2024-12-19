using AoC2024.Generic.HelperClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day6 : Day
    {
        char[,] inputGrid;
        Point currentDirection = new Point();
        WindDirection currentDirectionLetter;
        MovingObject myLocation;
        Point currentPosition;
        bool leftGrid = false;
        Dictionary<(int x, int y), List<WindDirection>> visitedInDirections = new Dictionary<(int x, int y), List<WindDirection>>();

        public Day6(int day) : base(day)
        {
            inputGrid = input.GetCharGrid();
        }

        public override void DoPart1()
        {
            GetStart();
            while (!leftGrid) 
            {
                TakeNextStep();
            }

            int totalpassed = 0;
            foreach (var item in inputGrid)
            {
                if (item == 'X')
                {
                    totalpassed++;
                }
            }
            Console.WriteLine(totalpassed);
        }

        public override void DoPart2()
        {
        }

        private void GetStart()
        {
            bool foundStart = false;
            for (int j = 0; j < inputGrid.GetLength(1); j++)
            {
                for (int i = 0; i < inputGrid.GetLength(0); i++)
                {
                    if (inputGrid[i, j] == '^')
                    {
                        myLocation = new MovingObject(new Point(0, -1), WindDirection.N);
                        currentDirection = new Point(0, -1);
                        currentDirectionLetter = WindDirection.N;
                        currentPosition = new Point(i, j);
                        foundStart = true;
                    }
                    if (inputGrid[i, j] == '>')
                    {
                        myLocation = new MovingObject(new Point(1, 0), WindDirection.E);
                        currentDirection = new Point(1, 0);
                        currentDirectionLetter = WindDirection.E;
                        currentPosition = new Point(i, j);
                        foundStart = true;
                    }
                    if (inputGrid[i, j] == 'v')
                    {
                        myLocation = new MovingObject(new Point(0, 1), WindDirection.S);
                        currentDirection = new Point(0, 1);
                        currentDirectionLetter = WindDirection.S;
                        currentPosition = new Point(i, j);
                        foundStart = true;
                    }
                    if (inputGrid[i, j] == '<')
                    {
                        myLocation = new MovingObject(new Point(-1, 0), WindDirection.W);
                        currentDirection = new Point(-1, 0);
                        currentDirectionLetter = WindDirection.W;
                        currentPosition = new Point(i, j);
                        foundStart = true;
                    }
                    if (foundStart) break;
                }
                if (foundStart) break;
            }           
        }

        private void GetNextDirection()
        {
            //switch (currentDirectionLetter)
            //{
            //    case WindDirection.N:
            //        currentDirectionLetter = WindDirection.E;
            //        currentDirection.X = 1;
            //        currentDirection.Y = 0;
            //        break;
            //    case WindDirection.E:
            //        currentDirectionLetter = WindDirection.S;
            //        currentDirection.X = 0;
            //        currentDirection.Y = 1;
            //        break;
            //    case WindDirection.S:
            //        currentDirectionLetter = WindDirection.W;;
            //        currentDirection.X = -1;
            //        currentDirection.Y = 0;
            //        break;
            //    case WindDirection.W:
            //        currentDirectionLetter = WindDirection.N;
            //        currentDirection.X = 0;
            //        currentDirection.Y = -1;
            //        break;
            //    default:
            //        break;
            //}

            //switch (myLocation.direction)
            //{
            //    case WindDirection.N:
            //        myLocation.direction = WindDirection.E;
            //        currentDirection.X = 1;
            //        currentDirection.Y = 0;
            //        break;
            //    case WindDirection.E:
            //        myLocation.direction = WindDirection.S;
            //        currentDirection.X = 0;
            //        currentDirection.Y = 1;
            //        break;
            //    case WindDirection.S:
            //        myLocation.direction = WindDirection.W; ;
            //        currentDirection.X = -1;
            //        currentDirection.Y = 0;
            //        break;
            //    case WindDirection.W:
            //        myLocation.direction = WindDirection.N;
            //        currentDirection.X = 0;
            //        currentDirection.Y = -1;
            //        break;
            //    default:
            //        break;
            //}
        }

        private void TakeNextStep()
        {
            inputGrid[currentPosition.X, currentPosition.Y] = 'X';
            Point nextPosition = new Point(currentPosition.X + currentDirection.X, currentPosition.Y + currentDirection.Y);
            if (nextPosition.X < 0 || nextPosition.Y < 0 || nextPosition.X > inputGrid.GetLength(0) - 1 || nextPosition.Y > inputGrid.GetLength(1) - 1)
            {
                leftGrid = true;
            }
            else if (inputGrid[nextPosition.X, nextPosition.Y] == '#')
            {
                GetNextDirection();
            }
            else
            {
                currentPosition = nextPosition;
                myLocation.position = nextPosition;

            }
            
        }
    }
}
