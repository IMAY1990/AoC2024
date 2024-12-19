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
    }
}
