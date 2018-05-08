using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NNProject1.GUI.UI.Common;

namespace NNProject1.GUI.UI.Windows
{
    /// <summary>
    /// Interaction logic for InputMatrixWindow.xaml
    /// </summary>
    public partial class InputMatrixWindow : Window, INotifyPropertyChanged
    {
        private int _n = 4;
        private int _m = 4;
        private List<MatrixCell> _cells = new List<MatrixCell>();

        public event PropertyChangedEventHandler PropertyChanged;

        public int N
        {
            get
            {
                return _n;
            }
            set
            {
                int oldN = _n;
                _n = value;
                OnPropertyChanged(nameof(N));
                Cells = Cells.UpdateMatrixCells(M, N, M, oldN);
            }
        }
        public int M
        {
            get
            {
                return _m;
            }
            set
            {
                int oldM = _m;
                _m = value;
                OnPropertyChanged(nameof(M));
                Cells = Cells.UpdateMatrixCells(M, N, oldM, N);
            }
        }
        public List<MatrixCell> Cells
        {
            get
            {
                return _cells;
            }
            set
            {
                _cells = value;
                OnPropertyChanged(nameof(Cells));
            }
        }

        private RelayCommand _updateSizeCommand;
        private RelayCommand _changeValueCommand;
        private RelayCommand _createMatrixCommand;

        public RelayCommand UpdateSizeCommand => _updateSizeCommand ?? (_updateSizeCommand = new RelayCommand(OnUpdateSizeClick));
        public RelayCommand ChangeValueCommand => _changeValueCommand ?? (_changeValueCommand = new RelayCommand(OnChangeValueClick));
        public RelayCommand CreateMatrixCommand => _createMatrixCommand ?? (_createMatrixCommand = new RelayCommand(OnCreateMatrixClick));

        public InputMatrixWindow()
        {
            Cells = MatrixCellExtension.CreateMatrixCells(M, N);    
            DataContext = this;
            InitializeComponent();         
        }

        private void OnUpdateSizeClick(object obj)
        {
            OnPropertyChanged(nameof(M));
            OnPropertyChanged(nameof(N));
        }

        private void OnChangeValueClick(object obj)
        {
            int pos = (int)obj;
            var cell = Cells[pos];
            int val = cell.Value == 1 ? -1 : 1;
            Cells[pos] = new MatrixCell(cell.Row, cell.Column, pos, val);
            Cells = new List<MatrixCell>(Cells);
        }

        private void OnCreateMatrixClick(object obj)
        {
            DialogResult = true;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
