using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day9 : Day
    {
        public Day9(int day) : base(day)
        {
        }

        public override void DoPart1()
        {
            string inputString = input.Rows[0];
            List<string> unoptimisedDisk = GetUnoptimisedDisk(inputString);
            List<string> optimisedDisk = GetOptimisedDisk(unoptimisedDisk);

            Console.WriteLine(CalculateChecksum(optimisedDisk));
        }

        public override void DoPart2()
        {
            string inputString = input.Rows[0];
            List<string> unoptimisedDisk = GetUnoptimisedDisk(inputString);
        }

        List<string> GetUnoptimisedDisk(string inputString) 
        {
            List<string> unoptimisedDisk = new List<string>();
            int nrOfFiles = 0;
            bool isFiles = true;
            for (int i = 0; i < inputString.Length; i++)
            {
                if (isFiles)
                {
                    for (int j = 0; j < Convert.ToInt32(inputString[i].ToString()); j++)
                    {
                        unoptimisedDisk.Add(nrOfFiles.ToString());
                    }
                    nrOfFiles++;
                    isFiles = false;
                }
                else
                {
                    for (int j = 0; j < Convert.ToInt32(inputString[i].ToString()); j++)
                    {
                        unoptimisedDisk.Add(".");
                    }
                    isFiles = true;
                }
            }

            return unoptimisedDisk;
        }

        List<string> GetOptimisedDisk(List<string> unoptimisedDisk) 
        { 
            List<string> optimisedDisk = new List<string>();
            while (unoptimisedDisk.Count > 0)
            {
                if (unoptimisedDisk.First() == ".")
                {
                    while (unoptimisedDisk.Last() == ".")
                    {
                        unoptimisedDisk.RemoveAt(unoptimisedDisk.Count - 1);
                    }
                    optimisedDisk.Add(unoptimisedDisk.Last());
                    unoptimisedDisk.RemoveAt(unoptimisedDisk.Count - 1);
                    unoptimisedDisk.RemoveAt(0);
                }
                else
                {
                    optimisedDisk.Add(unoptimisedDisk.First());
                    unoptimisedDisk.RemoveAt(0);
                }
            }

            return optimisedDisk;
        }

        List<string> GetOptimisedDisk2(List<string> unoptimisedDisk)
        {
            List<string> optimisedDisk = new List<string>();
            Stack<string> wontFit = new Stack<string>();

            while (unoptimisedDisk.Count != 0)
            {
                if (unoptimisedDisk.First() == ".")
                {
                    while (unoptimisedDisk.Last() == ".")
                    {
                        wontFit.Push(unoptimisedDisk.Last());
                        unoptimisedDisk.RemoveAt(unoptimisedDisk.Count - 1);
                    }

                    //Tel hoeveel space er is
                    int spaceCounter = 0;
                    for (int j = 0; j < unoptimisedDisk.Count(); j++)
                    {
                        if (unoptimisedDisk[j] == ".")
                        {
                            spaceCounter++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    //zoek naar een file op er in te stoppen
                    //check per file of het past Anders stack het op de wontFit stack
                    bool addedFile = false;
                    while (!addedFile)
                    {

                    }


                    optimisedDisk.Add(unoptimisedDisk.Last());
                    unoptimisedDisk.RemoveAt(unoptimisedDisk.Count - 1);
                    unoptimisedDisk.RemoveAt(0);
                }
                else
                {
                    optimisedDisk.Add(unoptimisedDisk.First());
                    unoptimisedDisk.RemoveAt(0);
                } 
            }

            return optimisedDisk;
        }

        long CalculateChecksum(List<string> optimisedDisk)
        {
            long sum = 0;

            for (int i = 0; i < optimisedDisk.Count; i++)
            {
                if (optimisedDisk[i] != ".")
                {
                    sum += Convert.ToInt64(optimisedDisk[i]) * i;
                }
            }

            return sum;
        }
    }
}
