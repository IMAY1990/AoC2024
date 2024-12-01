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

        private string line;
        private string filePath;
        private string fileName;

        internal Input(int day)
        {
            //input = new List<string>();
            this.dayNr = day;
            this.fileName = $"..//..//..//Inputs//input{dayNr}.txt";

            StreamReader reader = new StreamReader(fileName);
            line = reader.ReadLine();
            while (line != null)
            {
                Rows.Add(line);
                line = reader.ReadLine();
            }
            //close the file
            reader.Close();
        }

        

    }
}
