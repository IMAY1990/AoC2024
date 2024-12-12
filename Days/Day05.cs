using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Days
{
    internal class Day5 : Day
    {
        List<string> orderingRules = new List<string>();
        List<string> pagesToProduce = new List<string>();
        List<string> incorrectlyOrdered = new List<string>();
        public Day5(int day) : base(day)
        {
            foreach (var row in input.Rows)
            {
                if (row.Contains("|"))
                {
                    this.orderingRules.Add(row);
                }
                else if (row.Contains(","))
                {
                    this.pagesToProduce.Add(row);
                }
            }
        }

        public override void DoPart1()
        {
            int middleSum = 0;
            foreach (var pages in this.pagesToProduce)
            {
                bool isCorrectOrder = CheckOrder(pages);
                if (isCorrectOrder) middleSum += Convert.ToInt32(pages.Split(",")[pages.Split(",").Length / 2]);
                else incorrectlyOrdered.Add(pages);
            }

            Console.WriteLine(middleSum);
        }

        public override void DoPart2()
        {
            int middleSum = 0;
            foreach (var toBeSorted in incorrectlyOrdered)
            {
                List<string> sorted = ChangeOrder(toBeSorted);
                middleSum += Convert.ToInt32(sorted[sorted.Count/2]);
            }
            Console.WriteLine(middleSum);
        }

        private bool CheckOrder(string pages)
        {
            bool isCorrectOrder = true;
            List<string> pagesList = pages.Split(",").ToList();
            for (int i = 0; i < pagesList.Count; i++)
            {
                string page = pagesList[i];
                foreach (var rule in this.orderingRules.Where(x => x.EndsWith(page)))
                {
                    int firstIndex = pagesList.IndexOf(rule.Split("|")[0]);
                    if (firstIndex > i)
                    {
                        isCorrectOrder = false;
                        break;
                    }
                }
                if (!isCorrectOrder)
                {
                    break;
                }
            }
            return isCorrectOrder;
        }

        private List<string> ChangeOrder(string toBeSorted)
        {
            List<string> sortedList = new List<string>();
            List<string> pagesList = toBeSorted.Split(",").ToList();

            int iterations = 0;
            while (pagesList.Count > 0) 
            {
                for (int i = 0; i < pagesList.Count; i++)
                {
                    bool canBeAdded = true;
                    foreach (var rule in this.orderingRules.Where(x => x.EndsWith(pagesList[i])))
                    {
                        if (pagesList.Contains(rule.Split("|")[0]))
                        {
                            canBeAdded = false;
                            break;
                        }
                    }
                    if (canBeAdded)
                    {
                        sortedList.Add(pagesList[i]);
                        pagesList.RemoveAt(i);
                    }
                }
                iterations++;
            }
            return sortedList;
        }
    }
}
