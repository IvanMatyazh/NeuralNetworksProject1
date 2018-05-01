using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNProject1.DataModel
{
    /// <summary>
    /// Represents integer matrix A of size M x N 
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// Elements of the matrix
        /// </summary>
        public int[,] Values { get; protected set; }

        /// <summary>
        /// Number of rows in the matrix
        /// </summary>
        public int NRows { get; protected set; }

        /// <summary>
        /// Number of columns in the matrix
        /// </summary>
        public int NColumns { get; protected set; }

        protected Matrix() {}

        /// <summary>
        /// Constructs square Identity matrix of size n
        /// </summary>
        /// <param name="n">Number of rows</param>
        public Matrix(int n)
        {
            NRows = NColumns = n;
            Values = new int[n, n];
            for (int i = 0; i < n; i++)
                Values[i, i] = 1;
        }

        /// <summary>
        /// Constructs Zero matrix of size nrows by ncolumns
        /// </summary>
        /// <param name="nrows">Number of rows</param>
        /// <param name="ncolumns">Number of columns</param>
        public Matrix(int nrows, int ncolumns)
        {
            Values = new int[nrows, ncolumns];
            NRows = nrows;
            NColumns = ncolumns;
        }

        /// <summary>
        /// Constructs matrix from 2D array
        /// </summary>
        /// <param name="values">Elements of the matrix</param>
        public Matrix(int[,] values)
        {
            NRows = values.GetLength(0);
            NColumns = values.GetLength(1);
            Values = new int[NRows, NColumns];
            for (int i = 0; i < NRows; i++)
                for (int j = 0; j < NColumns; j++)
                    Values[i, j] = values[i, j];
        }

        /// <summary>
        /// Constructs Matrix from list of lists 
        /// </summary>
        /// <param name="values">Elements of the matrix</param>
        /// <param name="nrows">Maximal number of rows</param>
        /// <param name="ncolumns">Maximal number of columns</param>
        public Matrix(List<List<int>> values, int nrows, int ncolumns)
            : this(nrows, ncolumns)
        {
            for (int i = 0; i < nrows; i++)
            {
                for (int j = 0; j < ncolumns; j++)
                {
                    if (values.Count > i && values[i].Count > j)
                        Values[i, j] = values[i][j];
                    else
                        Values[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Constructs matrix from another matrix
        /// </summary>
        /// <param name="m">Input matrix</param>
        /// <param name="transpose">Flag indicates whether to transpose the matrix m</param>
        public Matrix(Matrix m, bool transpose)
        {
            NRows = transpose ? m.NColumns : m.NRows;
            NColumns = transpose ? m.NRows : m.NColumns;
            Values = new int[NRows, NColumns];
            for (int i = 0; i < NRows; i++)
                for (int j = 0; j < NColumns; j++)
                    Values[i, j] = transpose ? m.Values[j, i] : m.Values[i, j];
        }

        public int this[int i, int j]
        {
            get
            {
                if (i < NRows && j < NColumns)
                    return Values[i, j];
                else
                    throw new ArgumentOutOfRangeException("Indices of the matrix are out of range");
            }
            set
            {
                if (i < NRows && j < NColumns)
                    Values[i, j] = value;
                else
                    throw new ArgumentOutOfRangeException("Indices of the matrix are out of range");
            }
        }

        /// <summary>
        /// Scalar multiplication of matrix
        /// </summary>
        /// <param name="scale">Scalar</param>
        /// <param name="m">Matrix</param>
        /// <returns></returns>
        public static Matrix operator *(int scale, Matrix m)
        {
            Matrix res = new Matrix(m.NRows, m.NColumns);
            for (int i = 0; i < res.NRows; i++)
                for (int j = 0; j < res.NColumns; j++)
                    res[i, j] = scale * m[i, j];
            return res;
        }

        /// <summary>
        /// Substraction of matrices
        /// </summary>
        /// <param name="m1">First matrix</param>
        /// <param name="m2">Second matrix</param>
        /// <returns></returns>
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if ((m1.NRows == m2.NRows) && (m1.NColumns == m2.NColumns))
            {
                Matrix res = new Matrix(m1.NRows, m1.NColumns);
                for (int i = 0; i < res.NRows; i++)
                    for (int j = 0; j < res.NColumns; j++)
                        res[i, j] = m1[i, j] - m2[i, j];
                return res;
            }
            else
            {
                throw new ArgumentException("Matrices should have the same size");
            }
        }

        /// <summary>
        /// Sum of matrices
        /// </summary>
        /// <param name="m1">First matrix</param>
        /// <param name="m2">Second matrix</param>
        /// <returns></returns>
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if ((m1.NRows == m2.NRows) && (m1.NColumns == m2.NColumns))
            {
                Matrix res = new Matrix(m1.NRows, m1.NColumns);
                for (int i = 0; i < res.NRows; i++)
                    for (int j = 0; j < res.NColumns; j++)
                        res[i, j] = m1[i, j] + m2[i, j];
                return res;
            }
            else
            {
                throw new ArgumentException("Matrices should have the same size");
            }
        }

        /// <summary>
        /// Matrix by vector multipliplication
        /// </summary>
        /// <param name="m">Matrix</param>
        /// <param name="vect">Vector</param>
        /// <returns></returns>
        public static List<int> operator *(Matrix m, List<int> vect)
        {
            if (vect.Count == m.NColumns)
            {
                List<int> res = new List<int>();
                for (int i = 0; i < m.NRows; i++)
                {
                    int temp = 0;
                    for (int j = 0; j < m.NColumns; j++)
                    {
                        temp += m[i, j] * vect[j];
                    }
                    res.Add(temp);
                }
                return res;
            }
            else
            {
                throw new ArgumentException("The length of vector should be the same as number of columns in matrix");
            }
        }

        /// <summary>
        /// Matrix by vector multiplication
        /// </summary>
        /// <param name="m">Matrix</param>
        /// <param name="vect">Vector</param>
        /// <returns></returns>
        public static int[] operator *(Matrix m, int[] vect)
        {
            if (vect.Length == m.NColumns)
            {
                int[] res = new int[m.NRows];
                for (int i = 0; i < m.NRows; i++)
                {
                    int temp = 0;
                    for (int j = 0; j < m.NColumns; j++)
                    {
                        temp += m[i, j] * vect[j];
                    }
                    res[i] = temp;
                }
                return res;
            }
            else
            {
                throw new ArgumentException("The length of vector should be the same as number of columns in matrix");
            }
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="m1">First matrix</param>
        /// <param name="m2">Second matrix</param>
        /// <returns></returns>
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.NColumns == m2.NRows)
            {
                Matrix res = new Matrix(m1.NRows, m2.NColumns);
                for (int i = 0; i < res.NRows; i++)
                {
                    for (int j = 0; j < res.NColumns; j++)
                    {
                        int temp = 0;
                        for (int p = 0; p < m1.NColumns; p++)
                        {
                            temp += m1[i, p] * m2[p, j];
                        }
                        res[i, j] = temp;
                    }
                }
                return res;
            }
            else
            {
                throw new ArgumentException("# of columns of matrix m1 and # of rows of matrix m2 should be the same");
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < NRows; i++)
            {
                for (int j = 0; j < NColumns; j++)
                {
                    builder.Append(Values[i, j]);
                    builder.Append(" ");
                }
                builder.Append("\n");
            }
            return builder.ToString();
        }
    }
}
