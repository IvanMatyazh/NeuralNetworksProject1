using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNProject1.DataModel;
using System.IO;

namespace NNProject1.DataModel
{
    public class Input
    {
        public List<List<int>> Values { get; protected set; }

        public int VectorSize{ get; protected set; }

        public int NumberOfVectors { get; protected set; }

        protected Input() { }
        
        public Input(Stream fileStream)
        {
            Values = new List<List<int>>();
            string line;
            StreamReader reader = new StreamReader(fileStream);
            char[] charSeparators = new char[] { ',', ' ' };
            int currentVector = 0;
            using (fileStream)
            {
                line = reader.ReadLine();// Reading the header with the size of each vector
                VectorSize = int.Parse(line);
                while((line = reader.ReadLine()) != null )
                {
                    string[] lineSplit = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    if (lineSplit.Length == VectorSize)
                    {
                        Values.Add(ParseLine(lineSplit));
                        currentVector++;
                    }
                    else
                    {
                        fileStream.Close();
                        throw new ArgumentException("The vector at line: " + (currentVector + 1) + " should be of length: " + VectorSize + " instead of " + lineSplit.Length);
                    }
                }
            }
            NumberOfVectors = currentVector;
            fileStream.Close();
        }

        public Input(List<List<int>> values, int rows, int columns)
        {
            Values = new List<List<int>>();
            for (int i = 0; i < rows; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < columns; j++)
                {
                    row.Add(values[i][j]);
                }
                Values.Add(row);
            }
            NumberOfVectors = rows;
            VectorSize = columns;
        }

        private List<int> ParseLine(string[] elements)
        {
            List<int> tempVector = new List<int>(VectorSize);
            foreach (var elem in elements)
                tempVector.Add(int.Parse(elem));
            return tempVector;
        }
    }
}
