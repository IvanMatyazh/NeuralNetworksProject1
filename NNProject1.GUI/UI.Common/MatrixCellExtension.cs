using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNProject1.GUI.UI.Common
{
    public static class MatrixCellExtension
    {
        public static List<MatrixCell> CreateMatrixCells(int rows, int columns)
        {
            List<MatrixCell> cells = new List<MatrixCell>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    cells.Add(new MatrixCell(i, j, i * columns + j, 1));
                }
            }
            return cells;
        }

        public static List<MatrixCell> UpdateMatrixCells(this List<MatrixCell> oldCells, int rows, int columns, int oldRows, int oldColumns)
        {
            List<MatrixCell> cells = new List<MatrixCell>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i < oldRows && j < oldColumns)
                    {
                        var cell = oldCells[i * oldColumns + j];
                        cells.Add(new MatrixCell(i, j, i * columns + j, cell.Value));
                    }
                    else
                    {
                        cells.Add(new MatrixCell(i, j, i * columns + j, 1));
                    }
                }
            }
            return cells;
        }

        public static List<List<int>> ConvectFromMatrixCells(this List<MatrixCell> cells, int rows, int columns)
        {
            List<List<int>> values = new List<List<int>>();
            for (int i = 0; i < rows; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < columns; j++)
                {
                    row.Add(cells[i * columns + j].Value);
                }
                values.Add(row);
            }
            return values;
        }
    }
}
