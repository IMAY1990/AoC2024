using AoC2024.Days;
using AoC2024.Generic;

namespace AoC2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What day is it?");
            int dayNr = int.Parse(Console.ReadLine());

            switch (dayNr)
            {
                case 1:
                    Day1 day = new Day1(dayNr); 
                    break;
                default:
                    Console.WriteLine("invalid day");
                    break;
            }
        }
    }
}
