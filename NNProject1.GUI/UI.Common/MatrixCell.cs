using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNProject1.GUI.UI.Common
{
    public class MatrixCell
    {
        public int Position { get; set; }

        public int Value { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public MatrixCell(int row, int col, int pos, int val)
        {
            Row = row;
            Column = col;
            Position = pos;
            Value = val;
        }
    }
}
