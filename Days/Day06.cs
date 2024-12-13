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
        Point currentDirection;
        Point currentPosition;
        bool leftGrid = false;

        public Day6(int day) : base(day)
        {
            inputGrid = input.GetCharGrid();
        }

        public override void DoPart1()
        {
            GetStart();
            int testcounter = 0;
            while (!leftGrid) 
            {
                testcounter++;
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
                        currentDirection = new Point(0, -1);
                        currentPosition = new Point(i, j);
                        foundStart = true;
                    }
                    if (inputGrid[i, j] == '>')
                    {
                        currentDirection = new Point(1, 0);
                        currentPosition = new Point(i, j);
                        foundStart = true;
                    }
                    if (inputGrid[i, j] == 'v')
                    {
                        currentDirection = new Point(0, 1);
                        currentPosition = new Point(i, j);
                        foundStart = true;
                    }
                    if (inputGrid[i, j] == '<')
                    {
                        currentDirection = new Point(-1, 0);
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
            if (currentDirection == new Point(0, -1))//up
            {
                currentDirection = new Point(1, 0);//right
                return;
            }
            else if (currentDirection == new Point(1, 0))//right
            {
                currentDirection = new Point(0, 1);//down
                return;
            }
            else if (currentDirection == new Point(0, 1))//down
            {
                currentDirection = new Point(-1, 0);//left
                return;
            }
            else if (currentDirection == new Point(-1, 0))//left
            {
                currentDirection = new Point(0, -1);//up
                return;
            }
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
            }
            
        }
    }
}
