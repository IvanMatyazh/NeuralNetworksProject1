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
        public int vectorSize{ get; protected set; }
        public int numberOfVectors { get; protected set; }
        protected Input() { }
        
        public Input(Stream fileStream)
        {
            Values = new List<List<int>>();
            String line;
            StreamReader reader = new StreamReader(fileStream);
            char[] charSeparators = new char[] { ',', ' ' };
            int currentVector = 0;
            using (fileStream)
            {
                line = reader.ReadLine();// Reading the header with the size of each vector
                vectorSize = Int32.Parse(line);
                while((line = reader.ReadLine()) != null )
                {
                    string[] lineSplit = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    if (lineSplit.Length == vectorSize)
                    {
                        List<int> tempVector = new List<int>(vectorSize);
                        foreach (var elem in lineSplit)
                        {
                            tempVector.Add(Int32.Parse(elem));
                        }
                        Values.Add(tempVector);
                        currentVector++;
                    }
                    else
                    {
                        fileStream.Close();
                        throw new ArgumentException("The vector at line: " + (currentVector + 1) + " should be of length: " + vectorSize + " instead of " + lineSplit.Length);

                    }
                }
            }
            numberOfVectors = currentVector;
            fileStream.Close();

        }

    }
}
