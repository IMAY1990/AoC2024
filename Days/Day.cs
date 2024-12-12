using AoC2024.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    class Day
    {
        public Input input { get; set; }

        public Day(int day)
        {
            this.input = new Input(day);
        }

        public virtual void DoPart1() { }
        public virtual void DoPart2() { }

    }
}
