using AoC2024.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day1
    {
        private Input input { get; set; }
        private List<int> first { get; set; }
        private List<int> second { get; set; }
        public Day1() { }

        public Day1(int day) 
        {
            this.input = new Input(day);
            this.first = new List<int>();
            this.second = new List<int>();

            DoPart1();
            DoPart2();
        }

        void DoPart1()
        {
            foreach (var row in input.Rows)
            {
                string[] splitRow = row.Split("   ");
                first.Add(int.Parse(splitRow[0].Trim()));
                second.Add(int.Parse(splitRow[1].Trim()));
            }

            first.Sort();
            second.Sort();

            int totalDiff = 0;
            for (int i = 0; i < input.Rows.Count(); i++)
            {
                totalDiff += Math.Abs(first[i] - second[i]);
            }

            Console.WriteLine(totalDiff);
        }

        void DoPart2()
        {
            int similatiryScore = 0;
            foreach (int entry in first)
            {
                similatiryScore += (entry * second.Where(x => x == entry).Count());
            }

            Console.WriteLine(similatiryScore);
        }


    }
}
