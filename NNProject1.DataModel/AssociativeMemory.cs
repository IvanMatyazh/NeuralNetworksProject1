using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNProject1.DataModel
{
    /// <summary>
    /// Repesents Associative Memory based on the Hopfield matrix
    /// </summary>
    public class AssociativeMemory
    {
        /// <summary>
        /// Hopfield matrix constructs from input
        /// </summary>
        public HopfieldMatrix T { get; private set; }

        /// <summary>
        /// Number of maximum iterations
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// Input matrix
        /// </summary>
        private Matrix _input;

        /// <summary>
        /// Constructs Associative memory
        /// </summary>
        /// <param name="input">Input values</param>
        /// <param name="iterations">Maximum number of iterations</param>
        public AssociativeMemory(int[,] input, int iterations = 100)
        {
            _input = new Matrix(input);
            Iterations = iterations;
            T = new HopfieldMatrix(_input);
        }

        /// <summary>
        /// Constructs Associative memory
        /// </summary>
        /// <param name="input">Input values</param>
        /// <param name="nrows">Number of rows</param>
        /// <param name="ncolumns">Number of columns</param>
        /// <param name="iterations">Maximum number of iterations</param>
        public AssociativeMemory(List<List<int>> input, int nrows, int ncolumns, int iterations = 100)
        {
            _input = new Matrix(input, nrows, ncolumns);
            Iterations = iterations;
            T = new HopfieldMatrix(_input);
        }

        /// <summary>
        /// Performs Associative memory test for given vector
        /// </summary>
        /// <param name="vect">Test vector</param>
        /// <returns>Result of the test</returns>
        public TestResult Test(List<int> vect)
        {
            List<int> answer = CopyVector(vect);
            List<int> prevAnswer = CopyVector(vect);
            for(int i = 0; i < Iterations; i++)
            {
                var twoValuesCycle = CopyVector(prevAnswer);
                prevAnswer = CopyVector(answer);
                answer = T * answer;
                UpdateVector(answer);
                if (answer.SequenceEqual(prevAnswer))
                    return new TestResult(answer, true, i + 1);
                if (answer.SequenceEqual(twoValuesCycle))
                    return new TestResult(answer, prevAnswer, i + 1);
            }
            return new TestResult(answer, false, Iterations);
        }

        private void UpdateVector(List<int> vect)
        {
            for (int i = 0; i < vect.Count; i++)
                vect[i] = vect[i] >= 0 ? 1 : -1;
        }

        private List<int> CopyVector(List<int> vect)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < vect.Count; i++)
                result.Add(vect[i]);
            return result;
        }
    }
}
