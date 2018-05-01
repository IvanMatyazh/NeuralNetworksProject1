﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNProject1.DataModel
{
    /// <summary>
    /// Hopfield matrix generated by Hebb's rule
    /// </summary>
    public class HopfieldMatrix : Matrix
    {
        /// <summary>
        /// Constructs Hopfield matrix
        /// </summary>
        /// <param name="values">Input elements</param>
        /// <param name="nrows">Number of rows</param>
        /// <param name="ncolumns">Number of columns</param>
        public HopfieldMatrix(List<List<int>> values, int nrows, int ncolumns)
            : this(new Matrix(values, nrows, ncolumns))
        {
        }

        /// <summary>
        /// Constructs Hopfield matrix
        /// </summary>
        /// <param name="values">Input elements</param>
        public HopfieldMatrix(int[,] values)
            : this(new Matrix(values))
        {
        }

        /// <summary>
        /// Constructs Hopfield matrix
        /// </summary>
        /// <param name="mat">Input matrix</param>
        public HopfieldMatrix(Matrix mat)
        {
            if (IsValidInput(mat))
            {
                Matrix matT = new Matrix(mat, true);
                Matrix I = new Matrix(mat.NColumns);
                Matrix res = matT * mat - mat.NRows * I;
                NRows = res.NRows;
                NColumns = res.NColumns;
                Values = new int[NRows, NColumns];
                for (int i = 0; i < NRows; i++)
                    for (int j = 0; j < NColumns; j++)
                        Values[i, j] = res.Values[i, j]; 
            }
            else
            {
                throw new ArgumentException("It is not a Bipolar net");
            }
        }

        public new int this[int i, int j]
        {
            get
            {
                return base[i, j];
            }
        }

        private bool IsValidInput(Matrix m)
        {
            for (int i = 0; i < m.NRows; i++)
            {
                for (int j = 0; j < m.NColumns; j++)
                {
                    if (m[i, j] != -1 && m[i, j] != 1)
                        return false;
                }
            }
            return true;
        }
    }
}
