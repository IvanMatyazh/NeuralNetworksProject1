using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNProject1.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NNProject1.Tests
{
    [TestClass]
    public class AssociativeMemoryTests
    {
        int[,] input = { { 1, 1, 1, 1 },
                          { -1, -1, -1, -1 },
                          { 1, -1, 1, -1 } };

        List<int> vect = new List<int>(){ 1, 1, 1, 1 };

        List<int> vect2 = new List<int>() { 1, -1, 1, 1 };

        List<int> vect3 = new List<int>() { 1, 1, 1, -1 };

        [TestMethod]
        public void AssociateWithExistingVector()
        {
            AssociativeMemory mem = new AssociativeMemory(input, 10);
            TestResult result = mem.Test(vect);
            Assert.AreEqual(result.Result.SequenceEqual(vect), true);
            Assert.AreEqual(result.IsConvergent, true);
            Assert.AreEqual(result.IsTwoVectorsLoop, false);
            Assert.AreEqual(result.SecondVector.SequenceEqual(new List<int>()), true);
            Assert.AreEqual(result.NumberOfIterations, 1);
        }

        [TestMethod]
        public void AssociateWithVectorResultingInTwoVectorsLoop()
        {
            AssociativeMemory mem = new AssociativeMemory(input, 10);
            TestResult result = mem.Test(vect2);
            Assert.AreEqual(result.Result.SequenceEqual(vect2), true);
            Assert.AreEqual(result.IsConvergent, false);
            Assert.AreEqual(result.IsTwoVectorsLoop, true);
            Assert.AreEqual(result.SecondVector.SequenceEqual(vect3), true);
            Assert.AreEqual(result.NumberOfIterations, 2);
        }
    }
}
