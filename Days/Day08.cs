using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day8 : Day
    {
        char[,] antiNodeGrid;
        int gridWidth;
        int gridHeight;
        public Day8(int day) : base(day)
        {
        }

        public override void DoPart1()
        {
            gridWidth = input.Rows[0].Length;
            gridHeight = input.Rows.Count;
            antiNodeGrid = new char[gridWidth, gridHeight];
            for (int j = 0; j < gridHeight; j++)
            {
                for (int i = 0; i < gridWidth; i++)
                {
                    antiNodeGrid[i, j] = '.';
                }
            }

            Dictionary<char, List<(int X, int Y)>> nodeTypes = GetAllNodeTypes(gridWidth, gridHeight);

            foreach (var nodeType in nodeTypes)
            {
                for (int i = 0; i < nodeType.Value.Count; i++)
                {
                    for (int j = i + 1; j < nodeType.Value.Count; j++)
                    {
                        AddSingeAntiNodes(nodeType, i, j);
                    }
                }

            }

            int counter = 0;
            foreach (var position in antiNodeGrid)
            {
                if (position == '#')
                {
                    counter++;
                }
            }

            Console.WriteLine(counter);
        }

        public override void DoPart2()
        {
            Dictionary<char, List<(int X, int Y)>> nodeTypes = GetAllNodeTypes(gridWidth, gridHeight);
            for (int j = 0; j < gridHeight; j++)
            {
                for (int i = 0; i < gridWidth; i++)
                {
                    antiNodeGrid[i, j] = '.';
                }
            }

            foreach (var nodeType in nodeTypes)
            {
                for (int i = 0; i < nodeType.Value.Count; i++)
                {
                    for (int j = i+1; j < nodeType.Value.Count; j++)
                    {

                        AddInfiniteNodes(nodeType, i, j);
                        antiNodeGrid[nodeType.Value[i].X, nodeType.Value[i].Y] = '#';
                        antiNodeGrid[nodeType.Value[j].X, nodeType.Value[j].Y] = '#';
                    }
                }
            }

            int counter = 0;
            foreach (var position in antiNodeGrid)
            {
                if (position == '#')
                {
                    counter++;
                }
            }

            Console.WriteLine(counter);

        }

        private void AddSingeAntiNodes(KeyValuePair<char, List<(int X, int Y)>> nodeType, int i, int j)
        {
            int vectorX = 0; int vectorY = 0;
            vectorX = nodeType.Value[j].X - nodeType.Value[i].X;
            vectorY = nodeType.Value[j].Y - nodeType.Value[i].Y;
            (int X, int Y) antiNode1 = GetAntiNode(nodeType.Value[i], vectorX, vectorY);
            (int X, int Y) antiNode2 = GetAntiNode(nodeType.Value[j], -vectorX, -vectorY);

            if (antiNode1.X >= 0 && antiNode1.X < gridWidth &&
                antiNode1.Y >= 0 && antiNode1.Y < gridHeight)
            {
                antiNodeGrid[antiNode1.X, antiNode1.Y] = '#';
            }
            if (antiNode2.X >= 0 && antiNode2.X < gridWidth &&
                antiNode2.Y >= 0 && antiNode2.Y < gridHeight)
            {
                antiNodeGrid[antiNode2.X, antiNode2.Y] = '#';
            }
        }

        private void AddInfiniteNodes(KeyValuePair<char, List<(int X, int Y)>> nodeType, int i, int j)
        {
            int vectorX = 0; int vectorY = 0;
            vectorX = nodeType.Value[j].X - nodeType.Value[i].X;
            vectorY = nodeType.Value[j].Y - nodeType.Value[i].Y;
            (int X, int Y) antiNode1 = GetAntiNode(nodeType.Value[i], vectorX, vectorY);
            (int X, int Y) antiNode2 = GetAntiNode(nodeType.Value[j], -vectorX, -vectorY);
            bool addedNodes = AddAntiNodesToGrid(antiNode1, antiNode2);
            while (addedNodes)
            {
                (int X, int Y) newAntiNode1 = GetAntiNode(antiNode1, vectorX, vectorY);
                (int X, int Y) newAntiNode2 = GetAntiNode(antiNode2, -vectorX, -vectorY);
                antiNode1 = newAntiNode1;
                antiNode2 = newAntiNode2;
                addedNodes = AddAntiNodesToGrid(antiNode1, antiNode2);
            }

        }

        private static (int X, int Y) GetAntiNode((int X, int Y) nodePosition, int vectorX, int vectorY)
        {
            return (nodePosition.X - vectorX, nodePosition.Y - vectorY);
        }

        private bool AddAntiNodesToGrid((int X, int Y) antiNode1, (int X, int Y) antiNode2)
        {
            bool added = false;
            if (antiNode1.X >= 0 && antiNode1.X < gridWidth &&
                antiNode1.Y >= 0 && antiNode1.Y < gridHeight)
            {
                antiNodeGrid[antiNode1.X, antiNode1.Y] = '#';
                added = true;
            }
            if (antiNode2.X >= 0 && antiNode2.X < gridWidth &&
                antiNode2.Y >= 0 && antiNode2.Y < gridHeight)
            {
                antiNodeGrid[antiNode2.X, antiNode2.Y] = '#';
                added = true;
            }

            return added;
        }

        private Dictionary<char, List<(int X, int Y)>> GetAllNodeTypes(int gridWidth, int gridHeight)
        {
            Dictionary<char, List<(int X, int Y)>> nodeTypes = new Dictionary<char, List<(int X, int Y)>>();

            for (int j = 0; j < gridHeight; j++)
            {
                for (int i = 0; i < gridWidth; i++)
                {

                    if (input.Rows[j][i] != '.')
                    {
                        if (nodeTypes.ContainsKey(input.Rows[j][i]))
                        {
                            nodeTypes[input.Rows[j][i]].Add((i, j));
                        }
                        else
                        {
                            nodeTypes.Add(input.Rows[j][i], new List<(int X, int Y)>() { (i, j) });
                        }
                    }
                }
            }

            return nodeTypes;
        }

        private void DrawMap(char[,] grid)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    sb.Append(grid[i, j]);
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
