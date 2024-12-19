using AoC2024.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day19 : Day
    {
        public Day19(int day) : base(day)
        {
        }

        public override void DoPart1()
        {
            List<string> availableTowels = input.Rows[0].Split(", ").ToList();

            var start = DateTime.Now;
            var arrangements = FindAllArrangements();
            var answer = arrangements.Count(x => x > 0);
            Console.WriteLine(answer.ToString());
            Console.WriteLine(DateTime.Now - start);
        }

        public override void DoPart2()
        {
            var arrangements = FindAllArrangements();
            var answer = arrangements.Sum();
            Console.WriteLine(answer.ToString());
        }

        private List<long> FindAllArrangements()
        {
            // Parse
            List<string> towels = input.Rows[0].Split(", ").ToList();
            List<string> patterns = input.Rows[2..];

            // Find arrangements
            List<long> answers = new List<long>();
            for (int i = 0; i < patterns.Count; i++)
            {
                var pattern = patterns[i];
                Func<int, long> FindArrangements = _ => throw new Exception("Stub for memoized recursive function");
                FindArrangements = Memoization.Make((int index) =>
                {
                    if (index >= pattern.Length)
                    {
                        return 1L;
                    }
                    long possibleArrangements = 0;
                    foreach (var towel in towels)
                    {
                        if (pattern.AsSpan()[index..].StartsWith(towel))
                        {
                            var tails = FindArrangements(index + towel.Length);
                            possibleArrangements += tails;
                        }
                    }
                    return possibleArrangements;
                });
                var possible = FindArrangements(0);
                answers.Add(possible);
            }
            return answers;
        }
    }
}
