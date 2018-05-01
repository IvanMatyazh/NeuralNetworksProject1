using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNProject1.DataModel
{
    /// <summary>
    /// Represents result after associative memory test
    /// </summary>
    public class TestResult
    {
        /// <summary>
        /// Result vector
        /// </summary>
        public List<int> Result { get; private set; }

        /// <summary>
        /// Second vector in case of 2 vectors loop
        /// </summary>
        public List<int> SecondVector { get; private set; }

        /// <summary>
        /// Represents whether the result is convergent
        /// </summary>
        public bool IsConvergent { get; private set; }

        /// <summary>
        /// Represents whether it is 2 vectors loop case
        /// </summary>
        public bool IsTwoVectorsLoop { get; private set; }

        /// <summary>
        /// Number of executed iterations in the test
        /// </summary>
        public int NumberOfIterations { get; private set; }
   
        /// <summary>
        /// Constructs result of the test in 2 vectors loop case
        /// </summary>
        /// <param name="result">Result vector</param>
        /// <param name="secondVector">Second vector</param>
        /// <param name="iterations">Number of iterations</param>
        public TestResult(List<int> result, List<int> secondVector, int iterations)
        {
            IsConvergent = false;
            IsTwoVectorsLoop = true;
            Result = result;
            SecondVector = secondVector;
            NumberOfIterations = iterations;
        }

        /// <summary>
        /// Constructs result of the test
        /// </summary>
        /// <param name="result">Result vector</param>
        /// <param name="isConvergent">Is it convergent</param>
        /// <param name="iterations">Number of iterations</param>
        public TestResult(List<int> result, bool isConvergent, int iterations)
        {
            Result = result;
            SecondVector = new List<int>();
            IsConvergent = isConvergent;
            IsTwoVectorsLoop = false;
            NumberOfIterations = iterations;
        }
    }
}
