using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Generic
{
    namespace HelperClasses
    {
        enum Direction { N, E, S, W }

        public class Math 
        {

        }
        public class Machine
        {
            public (long X, long Y) btnA { get; set; }
            public (long X, long Y) btnB { get; set; }
            public (long X, long Y) prize { get; set; }
            public long btnAX { get; set; }
            public long btnAY { get; set; }
            public long btnBX { get; set; }
            public long btnBY { get; set; }
            public long prizeX { get; set; }
            public long prizeY { get; set; }

            public Machine(string buttonA, string buttonB, string prizepoint)
            {
                btnAX = Convert.ToInt64(buttonA.Split(": ")[1].Split(", ")[0].Substring(2));
                btnAY = Convert.ToInt64(buttonA.Split(": ")[1].Split(", ")[1].Substring(2));
                btnBX = Convert.ToInt64(buttonB.Split(": ")[1].Split(", ")[0].Substring(2));
                btnBY = Convert.ToInt64(buttonB.Split(": ")[1].Split(", ")[1].Substring(2));
                prizeX = Convert.ToInt64(prizepoint.Split(": ")[1].Split(", ")[0].Substring(2));
                prizeY = Convert.ToInt64(prizepoint.Split(": ")[1].Split(", ")[1].Substring(2));

                btnA = (Convert.ToInt64(buttonA.Split(": ")[1].Split(", ")[0].Substring(2)), Convert.ToInt64(buttonA.Split(": ")[1].Split(", ")[1].Substring(2)));
                btnB = (Convert.ToInt64(buttonB.Split(": ")[1].Split(", ")[0].Substring(2)), Convert.ToInt64(buttonB.Split(": ")[1].Split(", ")[1].Substring(2)));
                prize = (Convert.ToInt64(prizepoint.Split(": ")[1].Split(", ")[0].Substring(2)), Convert.ToInt64(prizepoint.Split(": ")[1].Split(", ")[1].Substring(2)));
            }
        }

    }
}
