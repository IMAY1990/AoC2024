using AoC2024.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day2: Day
    {
        public Day2(int day) : base(day)
        {
        }

        public override void DoPart1()
        {
            int valid = 0;
            foreach (var row in input.Rows)
            {
                string[] values = row.Split(" ");
                List<int> intValues = new List<int>();

                for (int i = 0; i < intValues.Count(); i++)
                {
                    intValues.Add(Convert.ToInt32(values[i]));
                }

                if (IsValid(intValues))
                {
                    valid++;
                }
            }

            Console.WriteLine(valid);
        }

        public override void DoPart2()
        {
            int valid = 0;
            foreach (var row in input.Rows)
            {
                string[] values = row.Split(" ");
                List<int> intValues = new List<int>();

                for (int i = 0; i < values.Length; i++)
                {
                    intValues.Add(Convert.ToInt32(values[i]));
                }

                if (IsValid(intValues))
                {
                    valid++;
                }
                else
                {
                    for (int i = 0; i < intValues.Count(); i++)
                    {
                        List<int> surpressed = new List<int>();
                        surpressed.AddRange(intValues);
                        surpressed.RemoveAt(i);

                        if (IsValid(surpressed))
                        {
                            valid++;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(valid);
        }

        private bool IsValid(List<int> intValues)
        {
            bool increasing = false;
            bool decreasing = false;

            for (int i = 0; i < intValues.Count() - 1; i++)
            {
                int diff = Math.Abs(intValues[i] - intValues[i + 1]);
                if (diff < 1 || diff > 3)
                {
                    return false;
                }
                if (intValues[i] < intValues[i + 1])
                {
                    increasing = true;
                    if (decreasing) return false;
                }
                if (intValues[i] > intValues[i + 1])
                {
                    decreasing = true;
                    if (increasing) return false;
                }
            }
            return true;
        }
    }
}
