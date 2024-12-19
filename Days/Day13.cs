using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using AoC2024.Generic;

namespace AoC2024.Days
{
    internal class Day13 : Day
    {
        List<Generic.HelperClasses.Machine> machines = new List<Generic.HelperClasses.Machine>();
        public Day13(int day) : base(day)
        {
            for (int i = 0; i < input.Rows.Count(); i+=4)
            {
                machines.Add(new Generic.HelperClasses.Machine(input.Rows[i], input.Rows[i + 1], input.Rows[i + 2]));
            }
        }

        public override void DoPart1()
        {
            long tokensNeeded = 0;
            foreach (var machine in machines)
            {
                long maxAPresses = Math.Min(machine.prize.X / machine.btnA.X, machine.prize.Y / machine.btnA.Y) + 1;
                long maxBPresses = Math.Min(machine.prize.X / machine.btnB.X, machine.prize.Y / machine.btnB.Y) + 1;
                List<(long aPresses, long bPresses)> solutions = FindAllSolutions(maxAPresses, maxBPresses, machine);

                tokensNeeded += FindCheapestSolution(solutions, maxAPresses, maxBPresses);
            }

            Console.WriteLine(tokensNeeded);
        }

        public override void DoPart2()
        {
            long tokensNeeded = 0;
            foreach (var machine in machines)
            {
                machine.prize = (machine.prize.X + 10000000000000, machine.prize.Y + 10000000000000);

                long maxAPresses = Math.Min(machine.prize.X / machine.btnA.X, machine.prize.Y / machine.btnA.Y) + 1;
                long maxBPresses = Math.Min(machine.prize.X / machine.btnB.X, machine.prize.Y / machine.btnB.Y) + 1;
                List<(long aPresses, long bPresses)> solutions = new List<(long aPresses, long bPresses)>();
                var sol = FindSomeSolution(machine);
                solutions.Add(sol);

                tokensNeeded += FindCheapestSolution(solutions, maxAPresses, maxBPresses);
            }

            Console.WriteLine(tokensNeeded);
        }

        private List<(long aPresses, long bPresses)> FindAllSolutions(long maxAPresses, long maxBPresses, Generic.HelperClasses.Machine machine)
        {
            List<(long aPresses, long bPresses)> solutions = new List<(long aPresses, long bPresses)>();
            for (long i = 0; i < maxAPresses; i++)
            {
                for (long j = 0; j < maxBPresses; j++)
                {
                    if (i * machine.btnA.X + j * machine.btnB.X == machine.prize.X &&
                        i * machine.btnA.Y + j * machine.btnB.Y == machine.prize.Y)
                    {
                        solutions.Add((i, j));
                        //Console.WriteLine($"Solution: {i}, {j}");
                        break;
                    }
                }
            }

            return solutions;
        }

        private long FindCheapestSolution(List<(long aPresses, long bPresses)> solutions, long maxAPresses, long maxBPresses)
        {
            long minTokensMachine = 0;
            if (solutions.Count > 0)
            {
                minTokensMachine = maxAPresses * 3 + maxBPresses;
                foreach (var solution in solutions)
                {
                    minTokensMachine = Math.Min(minTokensMachine, solution.aPresses * 3 + solution.bPresses);
                }
            }

            return minTokensMachine;
        }

        private (long aPresses, long bPresses) FindSomeSolution(Generic.HelperClasses.Machine machine)
        {
                var determinant = machine.btnA.X * machine.btnB.Y - machine.btnA.Y * machine.btnB.X;
                if (determinant is 0) return (0,0);
                var acNumerator = machine.prize.X * machine.btnB.Y - machine.prize.Y * machine.btnB.X;
                var bcNumerator = machine.prize.Y * machine.btnA.X - machine.prize.X * machine.btnA.Y;
                if (acNumerator % determinant is not 0 || bcNumerator % determinant is not 0) return (0,0);
                var ac = acNumerator / determinant;
                var bc = bcNumerator / determinant;
                return ac < 0 || bc < 0 ? (0,0) : (ac, bc);
        }

        
    }
}
