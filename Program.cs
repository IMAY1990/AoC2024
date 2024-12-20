﻿using AoC2024.Days;
using AoC2024.Generic;

namespace AoC2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What day is it?");
            int dayNr = int.Parse(Console.ReadLine());
            Day day = new Day(dayNr);

            switch (dayNr)
            {
                case 1:
                    day = new Day1(dayNr);
                    break;
                case 2:
                    day = new Day2(dayNr);
                    break;
                case 3:
                    day = new Day3(dayNr);
                    break;
                case 4:
                    day = new Day4(dayNr);
                    break;
                case 5:
                    day = new Day5(dayNr);
                    break;
                case 6:
                    day = new Day6(dayNr);
                    break;
                case 7:
                    day = new Day7(dayNr);
                    break;
                case 8:
                    day = new Day8(dayNr);
                    break;
                case 9:
                    day = new Day9(dayNr);
                    break;
                case 10:
                    day = new Day10(dayNr);
                    break;
                case 11:
                    day = new Day11(dayNr);
                    break;
                case 12:
                    day = new Day12(dayNr);
                    break;
                case 13:
                    day = new Day13(dayNr);
                    break;
                case 14:
                    day = new Day14(dayNr);
                    break;
                case 15:
                    day = new Day15(dayNr);
                    break;
                case 16:
                    day = new Day16(dayNr);
                    break;
                case 17:
                    day = new Day17(dayNr);
                    break;
                case 18:
                    day = new Day18(dayNr);
                    break;
                case 19:
                    day = new Day19(dayNr);
                    break;
                case 20:
                    day = new Day20(dayNr);
                    break;
                case 21:
                    day = new Day21(dayNr);
                    break;
                case 22:
                    day = new Day22(dayNr);
                    break;
                case 23:
                    day = new Day23(dayNr);
                    break;
                case 24:
                    day = new Day24(dayNr);
                    break;
                case 25:
                    day = new Day16(dayNr);
                    break;
                default:
                    Console.WriteLine("invalid day");
                    break;
            }

            day.DoPart1();
            day.DoPart2();
        }
    }
}
