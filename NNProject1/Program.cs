using NNProject1.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNProject1
{
    class Program
    {
        /// <summary>
        /// For development purposes only, later Console App should be removed
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Our Neural Network project");
            int[,] test = new int[,]
            {
                { 1, 1, 1, 1 },
                { -1, -1, -1, -1 },
                { 1, -1, 1, -1}
            };
            AssociativeMemory mem = new AssociativeMemory(test);
            Console.WriteLine("Hopfield Matrix:");
            Console.WriteLine(mem.T);
            var res = mem.Test(new List<int>() { 1, 1, 1, 1 });
            Console.WriteLine("Result after test:");
            for (int i = 0; i < res.Result.Count; i++)
               Console.Write(res.Result[i] + " ");
            Console.WriteLine();
        }
    }
}
