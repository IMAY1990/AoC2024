using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Generic
{
    namespace HelperClasses
    {
        public enum WindDirection { N, NE, E, SE, S, SW, W, NW }

        public class Direction
        {
            public WindDirection windDirection = new WindDirection();
            public (int X, int Y) direction = (0, 0);

            public Direction(WindDirection windDirection, (int X, int Y) direction)
            {
                this.windDirection = windDirection;
                this.direction = direction;
            }

            public Direction() { }
        }

        public static class MathHelper
        {
            public static int Gcd(int a, int b)
            {
                while (b != 0) (b, a) = (a % b, b);

                return a;
            }

            public static int Lcm(this IEnumerable<int> numbers){ return numbers.Aggregate(Lcm); }

            public static int Lcm(int a, int b)
            { 
                return b / Gcd(a, b) * a; 
            }

        }
        public class Machine
        {
            public (long X, long Y) btnA { get; set; }
            public (long X, long Y) btnB { get; set; }
            public (long X, long Y) prize { get; set; }

            public Machine(string buttonA, string buttonB, string prizepoint)
            {
                btnA = (Convert.ToInt64(buttonA.Split(": ")[1].Split(", ")[0].Substring(2)), Convert.ToInt64(buttonA.Split(": ")[1].Split(", ")[1].Substring(2)));
                btnB = (Convert.ToInt64(buttonB.Split(": ")[1].Split(", ")[0].Substring(2)), Convert.ToInt64(buttonB.Split(": ")[1].Split(", ")[1].Substring(2)));
                prize = (Convert.ToInt64(prizepoint.Split(": ")[1].Split(", ")[0].Substring(2)), Convert.ToInt64(prizepoint.Split(": ")[1].Split(", ")[1].Substring(2)));
            }
        }

        public class MovingObject
        {
            public (int X, int Y) position { get; set; }
            public Direction direction { get; set; }

            public MovingObject() 
            {
                this.position = (0, 0);
            }
            public MovingObject((int X, int Y) position, WindDirection windDirection)
            {
                this.position = position;
                this.direction.windDirection = windDirection;
            }
            
            public Direction GetNextDirection()
            {
                Direction dir = new Direction();
                switch (this.direction.windDirection)
                {
                    case WindDirection.N:
                        dir.windDirection = WindDirection.E;
                        break;
                    case WindDirection.E:
                        dir.windDirection = WindDirection.S;
                        break;
                    case WindDirection.S:
                        dir.windDirection = WindDirection.W;
                        break;
                    case WindDirection.W:
                        dir.windDirection = WindDirection.N;
                        break;
                    default:
                        break;
                }

                dir.direction = GetDirectionVector(dir.windDirection);

                return (dir);
            }

            public (int X, int Y) GetDirectionVector(WindDirection windDirection)
            {
                switch (windDirection)
                {
                    case WindDirection.N:
                        return (0, -1);
                    case WindDirection.E:
                        return (1, 0);
                    case WindDirection.S:
                        return (0, 1);
                    case WindDirection.W:
                        return (-1, 0);
                    default:
                        break;
                }

                return (0, 0);
            }
        }

        public static class Maps
        {
            public static (int sx, int sy) GetLocationOfChar(List<string> kaart, char character)
            {
                int sx = 0, sy = 0;
                for (int j = 0; j < kaart.Count; j++)
                {
                    var x = kaart[j].IndexOf(character);
                    if (x != -1)
                    {
                        (sx, sy) = (x, j);
                        break;
                    }
                }
                return (sx, sy);
            }

            public static List<(int X, int Y)> GetSurroundingReachable((int X, int Y) current, List<string> kaart, char endCharacter)
            {
                List<(int X, int Y)> surroundingReachable = new List<(int X, int Y)>();
                if (kaart[current.Y][current.X - 1] == '.' || kaart[current.Y][current.X - 1] == endCharacter)
                {
                    surroundingReachable.Add((current.X - 1, current.Y));
                }
                if (kaart[current.Y][current.X + 1] == '.' || kaart[current.Y][current.X + 1] == endCharacter)
                {
                    surroundingReachable.Add((current.X + 1, current.Y));
                }
                if (kaart[current.Y - 1][current.X] == '.' || kaart[current.Y - 1][current.X] == endCharacter)
                {
                    surroundingReachable.Add((current.X, current.Y - 1));
                }
                if (kaart[current.Y + 1][current.X] == '.' || kaart[current.Y + 1][current.X] == endCharacter)
                {
                    surroundingReachable.Add((current.X, current.Y + 1));
                }

                return surroundingReachable;
            }

            public static (bool foundEnd, Dictionary<(int X, int Y), int> reachableInSteps) GetStepsToExit(List<string> kaart)
            {
                Dictionary<(int X, int Y), int> reachableInSteps = new Dictionary<(int X, int Y), int>();
                int startX = 0; int startY = 0;
                (startX, startY) = Maps.GetLocationOfChar(kaart, 'S');
                int endX = 0; int endY = 0;
                (endX, endY) = Maps.GetLocationOfChar(kaart, 'E');

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
                        List<(int X, int Y)> reachableList = Maps.GetSurroundingReachable(current.Key, kaart, 'E');
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

                return (foundEnd, reachableInSteps);
            }
        }
    }
}
