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

namespace NNProject1.GUI.UI.Windows
{
    /// <summary>
    /// Interaction logic for InputMatrixWindow.xaml
    /// </summary>
    public partial class InputMatrixWindow : Window, INotifyPropertyChanged
    {
        private int _n = 4;
        private int _m = 4;
        private List<int> _elements = new List<int>();

        public event PropertyChangedEventHandler PropertyChanged;

        public int N
        {
            get
            {
                return _n;
            }
            set
            {
                _n = value;
                OnPropertyChanged(nameof(N));
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
                _m = value;
                OnPropertyChanged(nameof(M));
            }
        }
        public List<int> Elements
        {
            get
            {
                return _elements;
            }
            set
            {
                _elements = value;
                OnPropertyChanged(nameof(Elements));
            }
        }

        public InputMatrixWindow()
        {
            for (int i = 0; i < N*M; i++)
            {
                Elements.Add(i);
            }      
            DataContext = this;
            InitializeComponent();         
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
