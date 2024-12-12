using AoC2024.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day3 : Day
    {
        public Day3(int day) : base(day)
        {
        }

        public override void DoPart1()
        {
            long multSum = 0;
            foreach (var row in input.Rows)
            {
                multSum += GetMultSum(row);
            }

            Console.WriteLine(multSum);
        }

        public override void DoPart2()
        {
            string fullMemory = "";
            foreach (var row in input.Rows)
            {
                fullMemory += row;
            }
            Console.WriteLine(GetMultSum(fullMemory));

        }

        long GetMultSum(string row)
        {
            MatchCollection matches = GetAllMatches(row);

            long multSum = 0;
            bool toDo = true;

            foreach (Match match in matches)
            {
                switch (match.ToString())
                {
                    case "do()":
                        toDo = true;
                        break;
                    case "don't()":
                        toDo = false;
                        break;
                    default:
                        if (toDo)
                        {
                            string[] digits = ExtractDigits(match.ToString());
                            multSum += Convert.ToInt32(digits[0]) * Convert.ToInt32(digits[1]);
                        }
                        break;
                }
                
            }

            return multSum;
        }

        MatchCollection GetAllMatches(string row) 
        {
            string patern = "(mul\\(\\d{1,3}\\,\\d{1,3}\\))|(do\\(\\))|(don't\\(\\))";
            Regex regex = new Regex(patern);
            return regex.Matches(row);
        }

        string[] ExtractDigits(string command)
        {
            return command.Replace("mul(", "").Replace(")", "").Split(",");
        }
    }
}
