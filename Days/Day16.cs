using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AoC2024.Generic.HelperClasses;

namespace AoC2024.Days
{
    internal class Day16 : Day
    {
        char[,] inputGrid;
        public Day16(int day) : base(day)
        {
            this.inputGrid = input.GetCharGrid();
        }

        public override void DoPart1()
        {
            Console.WriteLine(RunMaze(input.Rows, true));
        }

        public override void DoPart2()
        {
            Console.WriteLine(RunMaze(input.Rows, false));
        }

        int RunMaze(List<string> kaart, bool returnLowestScore)
        {
            var height = kaart.Count;
            int startX = 0, startY = 0;
            (startX, startY) = GetStart();

            var lowestScore = int.MaxValue;
            var lowestScorePaths = new List<List<int>>();
            var visited = new Dictionary<int, int>();
            var trailheads = new Queue<(int x, int y, Direction dir, int score, List<int> path)>();

            visited[CellDirectionIndex(startX, startY, Direction.E, height)] = 0;
            trailheads.Enqueue((startX, startY, Direction.E, 0, [CellIndex(startX, startY, height)]));

            while (trailheads.Count > 0)
            {
                var trail = trailheads.Dequeue();
                int dxanticlock, dyanticlock, dx, dy, dxclock, dyclock;
                TrailDirectionChange(trail, out dxanticlock, out dyanticlock, out dx, out dy, out dxclock, out dyclock);
                ExtendTrail(trail.x + dxanticlock, trail.y + dyanticlock, RotateAntiClock90(trail.dir), trail.score + 1001, kaart, height, ref lowestScore, lowestScorePaths, visited, trailheads, trail);
                ExtendTrail(trail.x + dx, trail.y + dy, trail.dir, trail.score + 1, kaart, height, ref lowestScore, lowestScorePaths, visited, trailheads, trail);
                ExtendTrail(trail.x + dxclock, trail.y + dyclock, RotateClock90(trail.dir), trail.score + 1001, kaart, height, ref lowestScore, lowestScorePaths, visited, trailheads, trail);
            }

            return returnLowestScore ? lowestScore : lowestScorePaths.SelectMany(p => p).Distinct().Count() + 1; // +1 for E
        }

        private static int CellDirectionIndex(int x, int y, Direction d, int h) => y * 4 + x * h * 4 + (int)d;

        private static int CellIndex(int x, int y, int h) => y * h + x;

        private void ExtendTrail(
            int x, 
            int y, 
            Direction dir, 
            int score, 
            List<string> kaart, 
            int height, 
            ref int lowestScore, 
            List<List<int>>? lowestScorePaths, 
            Dictionary<int, int>? visited, 
            Queue<(int x, int y, Direction dir, int score, List<int> path)>? trailheads, 
            (int x, int y, Direction dir, int score, List<int> path) trail)
        {
            var c = kaart[y][x];

            if (c == '#') return;

            if (c == 'E')
            {
                if (score <= lowestScore)
                {
                    if (score < lowestScore) lowestScorePaths.Clear(); // start over
                    lowestScore = score;
                    lowestScorePaths.Add(trail.path);
                }
                return;
            }

            if (visited.TryGetValue(CellDirectionIndex(x, y, dir, kaart.Count), out var visitedScore) && visitedScore < score) return;

            visited[CellDirectionIndex(x, y, dir, kaart.Count)] = score;
            trailheads.Enqueue((x, y, dir, score, [.. trail.path, CellIndex(x, y, kaart.Count)]));
        }

        private void TrailDirectionChange(
            (int x, int y, Direction dir, int score, List<int> path) trail, 
            out int dxcc, 
            out int dycc, 
            out int dx, 
            out int dy, 
            out int dxc, 
            out int dyc)
        {
            switch (trail.dir)
            {
                case Direction.N:
                    dxcc = -1;
                    dycc = 0;
                    dx = 0;
                    dy = -1;
                    dxc = 1;
                    dyc = 0;
                    break;
                case Direction.E:
                    dxcc = 0;
                    dycc = -1;
                    dx = 1;
                    dy = 0;
                    dxc = 0;
                    dyc = 1;
                    break;
                case Direction.S:
                    dxcc = 1;
                    dycc = 0;
                    dx = 0;
                    dy = 1;
                    dxc = -1;
                    dyc = 0;
                    break;
                case Direction.W:
                    dxcc = 0;
                    dycc = 1;
                    dx = -1;
                    dy = 0;
                    dxc = 0;
                    dyc = -1;
                    break;
                default:
                    dxcc = 0;
                    dycc = 0;
                    dx = 0;
                    dy = 0;
                    dxc = 0;
                    dyc = 0;
                    break;
            }
        }

        Direction RotateClock90(Direction d) 
        { 
            return (Direction)(((int)d + 1) % 4);
        }
        Direction RotateAntiClock90(Direction d)
        {
            return (Direction)((((int)d - 1) % 4 + 4) % 4);
        }
        (int sx, int sy) GetStart()
        {
            int sx = 0, sy = 0;
            for (int j = 0; j < input.Rows.Count; j++)
            {
                var x = input.Rows[j].IndexOf('S');
                if (x != -1)
                {
                    (sx, sy) = (x, j);
                    break;
                }
            }
            return (sx, sy);
        }

    }
}
