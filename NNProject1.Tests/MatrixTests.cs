using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNProject1.DataModel;
using System.Collections.Generic;

namespace NNProject1.Tests
{
    [TestClass]
    public class MatrixTests
    {
        int n = 3;

        int[,] I = { {1, 0, 0},
                     {0, 1, 0},
                     {0, 0, 1} };

        int[,] arr = { { 1, 2 },
                       { 3, 4 },
                       { 5, 6 } };

        int[] vect = { 1, 1 };

        List<int> vect2 = new List<int>() { 1, 1 };

        int[] multResultVector = { 3, 7, 11 };

        int[,] arr2 = { { 2, 3 },
                        { 4, 5 },
                        { 6, 7 } };

        int[,] arr3 = { { 1, 2, 3},
                        { 4, 5, 6} };

        int[,] multResultMatrix = { { 9, 12, 15 },
                                    { 19, 26, 33 },
                                    { 29, 40, 51 } };

        [TestMethod]
        public void ConstructIdentityMatrix()
        {
            Matrix A = new Matrix(n);
            Assert.AreEqual(A.NRows, n);
            Assert.AreEqual(A.NColumns, n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Assert.AreEqual(A[i, j], I[i, j]); 
        }

        [TestMethod]
        public void ConstructMatrixFrom2DArray()
        {
            Matrix A = new Matrix(arr);
            Assert.AreEqual(A.NRows, 3);
            Assert.AreEqual(A.NColumns, 2);
            for (int i = 0; i < A.NRows; i++)
                for (int j = 0; j < A.NColumns; j++)
                    Assert.AreEqual(A[i, j], arr[i, j]);
        }

        [TestMethod]
        public void ConstructRectangularMatrix()
        {
            Matrix A = new Matrix(3, 4);
            Assert.AreEqual(A.NRows, 3);
            Assert.AreEqual(A.NColumns, 4);
            for (int i = 0; i < A.NRows; i++)
                for (int j = 0; j < A.NColumns; j++)
                    Assert.AreEqual(A[i, j], 0);
        }

        [TestMethod]
        public void ConstructMatrixFromListOfLists()
        {
            List<List<int>> list = new List<List<int>>();
            for (int j = 0; j < 4; j++)
            {
                List<int> values = new List<int>();
                for (int i = 0; i < 3; i++)
                    values.Add(i);
                list.Add(values);
            }
            Matrix A = new Matrix(list, 4, 3);
            Assert.AreEqual(A.NRows, 4);
            Assert.AreEqual(A.NColumns, 3);
            for (int i = 0; i < A.NRows; i++)
                for (int j = 0; j < A.NColumns; j++)
                    Assert.AreEqual(A[i, j], list[i][j]);
        }

        [TestMethod]
        public void ConstructMatrixFromAnotherMatrix()
        {
            Matrix A = new Matrix(arr);
            Matrix Acopy = new Matrix(A, false);
            Assert.AreEqual(Acopy.NRows, A.NRows);
            Assert.AreEqual(Acopy.NColumns, A.NColumns);
            for (int i = 0; i < A.NRows; i++)
                for (int j = 0; j < A.NColumns; j++)
                    Assert.AreEqual(Acopy[i, j], A[i, j]);
        }

        [TestMethod]
        public void ConstructMatrixFromAnotherMatrixTransposed()
        {
            Matrix A = new Matrix(arr);
            Matrix Acopy = new Matrix(A, true);
            Assert.AreEqual(Acopy.NRows, A.NColumns);
            Assert.AreEqual(Acopy.NColumns, A.NRows);
            for (int i = 0; i < Acopy.NRows; i++)
                for (int j = 0; j < Acopy.NColumns; j++)
                    Assert.AreEqual(Acopy[i, j], A[j, i]);
        }

        [TestMethod]
        public void ConstructMatrixFromJaggedListOfLists()
        {
            List<List<int>> list = new List<List<int>>();
            for (int i = 0; i < 3; i++)
            {
                List<int> values = new List<int>();
                for (int j = 0; j < i + 1; j++)
                    values.Add(i + 1);
                list.Add(values);
            }
            Matrix A = new Matrix(list, 3, 3);
            Assert.AreEqual(A.NRows, 3);
            Assert.AreEqual(A.NColumns, 3);
            for (int i = 0; i < A.NRows; i++)
                for (int j = 0; j < A.NColumns; j++)
                    Assert.AreEqual(A[i, j], j < i + 1 ? list[i][j] : 0);
        }

        [TestMethod]
        public void ScaleMatrix()
        {
            Matrix A = new Matrix(arr);
            Matrix res = 5 * A;
            Assert.AreEqual(res.NRows, A.NRows);
            Assert.AreEqual(res.NColumns, A.NColumns);
            for (int i = 0; i < res.NRows; i++)
                for (int j = 0; j < res.NColumns; j++)
                    Assert.AreEqual(res[i, j], arr[i, j] * 5);
        }

        [TestMethod]
        public void SumMatrices()
        {
            Matrix A = new Matrix(arr);
            Matrix B = new Matrix(arr2);
            Matrix res = A + B;
            Assert.AreEqual(res.NRows, A.NRows);
            Assert.AreEqual(res.NColumns, A.NColumns);
            for (int i = 0; i < res.NRows; i++)
                for (int j = 0; j < res.NColumns; j++)
                    Assert.AreEqual(res[i, j], arr[i, j] + arr2[i, j]);
        }

        [TestMethod]
        public void SubtractMatrices()
        {
            Matrix A = new Matrix(arr);
            Matrix B = new Matrix(arr2);
            Matrix res = A - B;
            Assert.AreEqual(res.NRows, A.NRows);
            Assert.AreEqual(res.NColumns, A.NColumns);
            for (int i = 0; i < res.NRows; i++)
                for (int j = 0; j < res.NColumns; j++)
                    Assert.AreEqual(res[i, j], arr[i, j] - arr2[i, j]);
        }

        [TestMethod]
        public void MultiplyMatrixByVector()
        {
            Matrix A = new Matrix(arr);
            List<int> res = A * vect2;
            Assert.AreEqual(res.Count, multResultVector.Length);
            for (int i = 0; i < vect2.Count; i++)
                Assert.AreEqual(res[i], multResultVector[i]);
        }

        [TestMethod]
        public void MultiplyMatrixByArray()
        {
            Matrix A = new Matrix(arr);
            int[] res = A * vect;
            Assert.AreEqual(res.Length, multResultVector.Length);
            for (int i = 0; i < vect.Length; i++)
                Assert.AreEqual(res[i], multResultVector[i]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MultiplyMatrixByVectorOfNotEqualSize()
        {
            Matrix A = new Matrix(arr);
            List<int> testVect = new List<int> { 1, 1, 1 };
            List<int> res = A * testVect;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MultiplyMatrixByArrayOfNotEqualSize()
        {
            Matrix A = new Matrix(arr);
            int[] testVect = new int[3];
            int[] res = A * testVect;
        }

        [TestMethod]
        public void MultiplyMatrixByMatrix()
        {
            Matrix A = new Matrix(arr);
            Matrix B = new Matrix(arr3);
            Matrix res = A * B;
            Assert.AreEqual(res.NRows, A.NRows);
            Assert.AreEqual(res.NColumns, B.NColumns);
            for (int i = 0; i < res.NRows; i++)
                for (int j = 0; j < res.NColumns; j++)
                    Assert.AreEqual(res[i, j], multResultMatrix[i, j]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MultiplyMatrixByMatrixOfDifferentSizes()
        {
            Matrix A = new Matrix(arr);
            Matrix B = new Matrix(arr2);
            Matrix res = A * B;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SubtractMatricesWithNotEqualSize()
        {
            Matrix A = new Matrix(arr);
            Matrix B = new Matrix(arr3);
            Matrix res = A - B;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SumMatricesWithNotEqualSize()
        {
            Matrix A = new Matrix(arr);
            Matrix B = new Matrix(arr3);
            Matrix res = A + B;
        }

        [TestMethod]
        public void RetriveValueOfMatrix()
        {
            Matrix A = new Matrix(arr);
            Assert.AreEqual(A[1, 1], 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RetriveValueOutOfRangeOfMatrix()
        {
            Matrix A = new Matrix(arr);
            int value = A[5, 5];
        }
    }
}
