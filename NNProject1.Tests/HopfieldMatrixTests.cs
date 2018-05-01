using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNProject1.DataModel;
using System.Collections.Generic;

namespace NNProject1.Tests
{
    [TestClass]
    public class HopfieldMatrixTests
    {
        int[,] input1 = { { 1, 1, 1, 1 },
                          { -1, -1, -1, -1 },
                          { 1, -1, 1, -1 } };

        int[,] input2 = { { 1, 1, 1, 1 },
                          { -1, -1, -1, -1 },
                          { 1, -1, 2, -1 } };

        int[,] result = { { 0, 1, 3, 1 },
                          { 1, 0, 1, 3 },
                          { 3, 1, 0, 1 },
                          { 1, 3, 1, 0 } };

        List<List<int>> listInput = new List<List<int>>()
        {
            new List<int>() { 1, 1, 1, 1 },
            new List<int>() { -1, -1, -1, -1 },
            new List<int>() { 1, -1, 1, -1 }
        };

        List<List<int>> listInput2 = new List<List<int>>()
        {
            new List<int>() { 1, 1, 1, 1 },
            new List<int>() { -1, -2, -1, -1 },
            new List<int>() { 1, -1, 1, -1 }
        };

        [TestMethod]
        public void ConstructHopfieldMatrixFrom2DArray()
        {
            HopfieldMatrix mat = new HopfieldMatrix(input1);
            Assert.AreEqual(mat.NRows, result.GetLength(0));
            Assert.AreEqual(mat.NColumns, result.GetLength(1));
            for (int i = 0; i < mat.NRows; i++)
                for (int j = 0; j < mat.NColumns; j++)
                    Assert.AreEqual(mat[i, j], result[i, j]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructHopfieldMatrixFrom2DArrayInvalidInput()
        {
            HopfieldMatrix mat = new HopfieldMatrix(input2);
        }

        [TestMethod]
        public void ConstructHopfieldMatrixFromListOfLists()
        {
            HopfieldMatrix mat = new HopfieldMatrix(listInput, 3, 4);
            Assert.AreEqual(mat.NRows, result.GetLength(0));
            Assert.AreEqual(mat.NColumns, result.GetLength(1));
            for (int i = 0; i < mat.NRows; i++)
                for (int j = 0; j < mat.NColumns; j++)
                    Assert.AreEqual(mat[i, j], result[i, j]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructHopfieldMatrixFromListOfListsInvalidInput()
        {
            HopfieldMatrix mat = new HopfieldMatrix(listInput2, 3, 4);
        }

        [TestMethod]
        public void ConstructHopfieldMatrixFromMatrix()
        {
            HopfieldMatrix mat = new HopfieldMatrix(new Matrix(input1));
            Assert.AreEqual(mat.NRows, result.GetLength(0));
            Assert.AreEqual(mat.NColumns, result.GetLength(1));
            for (int i = 0; i < mat.NRows; i++)
                for (int j = 0; j < mat.NColumns; j++)
                    Assert.AreEqual(mat[i, j], result[i, j]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructHopfieldMatrixFromMatrixInvalidInput()
        {
            HopfieldMatrix mat = new HopfieldMatrix(new Matrix(input2));
        }

        [TestMethod]
        public void RetriveValueOfHopfieldMatrix()
        {
            Matrix A = new Matrix(input1);
            Assert.AreEqual(A[1, 1], -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RetriveValueOutOfRangeOfHopfieldMatrix()
        {
            Matrix A = new Matrix(input1);
            int value = A[5, 5];
        }
    }
}
