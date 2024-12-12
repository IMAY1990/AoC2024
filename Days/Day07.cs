using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day7 : Day
    {
        public Day7(int day) : base(day)
        {
        }

        public override void DoPart1()
        {
            long possibleSum = 0;
            foreach (var row in input.Rows)
            {
                string[] splitInput = row.Split(": ");
                long outcome = Convert.ToInt64(splitInput[0]);

                string[] inputNumbers = splitInput[1].Split(" ");
                List<long> possibleOutcomes = new List<long>() { Convert.ToInt64(inputNumbers[0]) };
                for (int i = 1; i < inputNumbers.Length; i++)
                {
                    possibleOutcomes = GetPossibleValues("+,*", possibleOutcomes, inputNumbers[i]);
                }
                if (possibleOutcomes.Contains(outcome))
                {
                    possibleSum += outcome;
                }
            }

            Console.WriteLine(possibleSum);
        }

        public override void DoPart2()
        {
            long possibleSum = 0;
            foreach (var row in input.Rows)
            {
                string[] splitInput = row.Split(": ");
                long outcome = Convert.ToInt64(splitInput[0]);

                string[] inputNumbers = splitInput[1].Split(" ");
                List<long> possibleOutcomes = new List<long>() { Convert.ToInt64(inputNumbers[0]) };
                for (int i = 1; i < inputNumbers.Length; i++)
                {
                    possibleOutcomes = GetPossibleValues("+,*,||", possibleOutcomes, inputNumbers[i]);
                }
                if (possibleOutcomes.Contains(outcome))
                {
                    possibleSum += outcome;
                }
            }

            Console.WriteLine(possibleSum);
        }

        List<long> GetPossibleValues(string operations, List<long> results, string nextNumber)
        {
            List<long> values = new List<long>();
            values.AddRange(results);

            results = new List<long>();
            int number = Convert.ToInt32(nextNumber);
            foreach (var value in values)
            {
                if (operations.Contains('+'))
                    results.Add(number + value);
                if (operations.Contains('*'))
                    results.Add(number * value);
                if (operations.Contains("||"))
                {
                    string concatted = value.ToString() + nextNumber;
                    results.Add(Convert.ToInt64(concatted));
                }
                    
            }

            return results;
        }
    }
}
