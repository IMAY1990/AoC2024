using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024.Generic
{
    internal class Input
    {
        public List<string> Rows { get; set; } = new List<string>();
        private int dayNr {  get; set; }

        private string fileName;

        internal Input(int day)
        {
            this.dayNr = day;
            this.fileName = $".//Inputs//input{dayNr}.txt";

            StreamReader reader = new StreamReader(fileName);
            string line = reader.ReadLine();
            while (line != null)
            {
                Rows.Add(line);
                line = reader.ReadLine();
            }
            //close the file
            reader.Close();
        }

        internal List<string> AddPadding(int paddingAmount, char padChar)
        {
            List<string> paddedInput = new List<string>();
            string padding = new StringBuilder().Append(padChar, Rows[0].Length + 2 * paddingAmount).ToString();
            for (int i = 0; i < paddingAmount; i++)
            {
                paddedInput.Add(padding);
            }
            foreach (var row in this.Rows)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(padChar, paddingAmount);
                sb.Append(row);
                sb.Append(padChar, paddingAmount);
                paddedInput.Add(sb.ToString());
            }
            for (int i = 0; i < paddingAmount; i++)
            {
                paddedInput.Add(padding);
            }

            return paddedInput;
        }

        internal int[,] GetIntGrid()
        {
            int[,] intGrid = new int[this.Rows[0].Length, this.Rows.Count];

            for (int j = 0; j < this.Rows.Count; j++)
            {
                for (int i = 0; i < this.Rows[j].Length; i++)
                {
                    if (this.Rows[j][i] == '.')
                        intGrid[i, j] = 100;
                    else
                        intGrid[i, j] = Convert.ToInt32(this.Rows[j][i].ToString());
                }
            }

            return intGrid;
        }
    }
}
