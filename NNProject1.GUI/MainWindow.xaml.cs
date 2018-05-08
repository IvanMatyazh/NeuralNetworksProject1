using NNProject1.DataModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NNProject1.GUI.UI.Common;
using System.ComponentModel;
using NNProject1.GUI.UI.Windows;

namespace NNProject1.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Input _input;
        private AssociativeMemory _associativeMemory;
        private List<int> _testVector;
        private bool _isEnabledTestVectorTextBox = false;
        private string _testVectorText = "Please add a test vector";
        private string _consoleText = string.Empty;
        private ConsoleWriter _writer;

        public bool IsEnabledTestVectorTextBox
        {
            get
            {
                return _isEnabledTestVectorTextBox;
            }
            set
            {
                _isEnabledTestVectorTextBox = value;
                OnPropertyChanged(nameof(IsEnabledTestVectorTextBox));
            }
        }

        public string TestVectorText
        {
            get
            {
                return _testVectorText;
            }
            set
            {
                _testVectorText = value;
                OnPropertyChanged(nameof(TestVectorText));
            }
        }

        public string ConsoleText
        {
            get
            {
                return _consoleText;
            }
            set
            {
                _consoleText = value;
                OnPropertyChanged(nameof(ConsoleText));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region Commands
        private RelayCommand _openFileCommand;
        private RelayCommand _exitCommand;
        private RelayCommand _createMemoryCommand;
        private RelayCommand _testVectorCommand;
        private RelayCommand _inputMatrixCommand;

        public RelayCommand OpenFileCommand => _openFileCommand ?? (_openFileCommand = new RelayCommand(OnOpenFileClick));
        public RelayCommand ExitCommand => _exitCommand ?? (_exitCommand = new RelayCommand(OnExitClick));
        public RelayCommand CreateMemoryCommand => _createMemoryCommand ?? (_createMemoryCommand = new RelayCommand(OnCreateMemoryClick, CanExecuteCreateMemoryClick));
        public RelayCommand TestVectorCommand => _testVectorCommand ?? (_testVectorCommand = new RelayCommand(OnTestVectorClick, CanExecuteTestVectorClick));
        public RelayCommand InputMatrixCommand => _inputMatrixCommand ?? (_inputMatrixCommand = new RelayCommand(OnInputMatrixClick));
        #endregion

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            _writer = new ConsoleWriter();
            _writer.Write("Welcome to our Neural Networks Project");
            ConsoleText = _writer.Text;
        }

        private void OnExitClick(object obj)
        {
            Application.Current.Shutdown();
        }

        private void OnOpenFileClick(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _input = new Input(openFileDialog.OpenFile());
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error creating input class" + ex.Message);
                }
                finally
                {
                    _writer.Write(_input);
                    ConsoleText = _writer.Text;
                }
            }
        }

        private bool CanExecuteCreateMemoryClick(object obj)
        {
            return _input != null;
        }

        private void OnCreateMemoryClick(object obj)
        {
            try
            {
                _associativeMemory = new AssociativeMemory(_input.Values, _input.NumberOfVectors, _input.VectorSize);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error creating associative memory" + ex.Message);
            }
            finally
            {
                if(_associativeMemory != null)
                {
                    _writer.Write("Associative memory created");
                    _writer.Write("Hopfield Matrix");
                    _writer.Write(_associativeMemory);
                    _writer.Write("Now you can input a test vector of length " + _input.VectorSize);
                    TestVectorText = string.Empty;
                    IsEnabledTestVectorTextBox = true;
                    ConsoleText = _writer.Text;
                }               
            }
        }

        private void OnInputMatrixClick(object obj)
        {
            var window = new InputMatrixWindow();
            if(window.ShowDialog() == true)
            {
                try
                {
                    var values = window.Cells.ConvectFromMatrixCells(window.M, window.N);
                    _input = new Input(values, window.M, window.N);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating input class" + ex.Message);
                }
                finally
                {
                    _writer.Write(_input);
                    ConsoleText = _writer.Text;
                }
            }
        }

        private bool CanExecuteTestVectorClick(object obj)
        {
            return _associativeMemory != null;
        }

        private void OnTestVectorClick(object obj)
        {
            _testVector = new List<int>();
            char[] charSeparators = new char[] { ',', ' ' };
            if (!string.IsNullOrEmpty(TestVectorText))
            {
                string[] lineSplit = TestVectorText.Split(charSeparators,StringSplitOptions.RemoveEmptyEntries);
                if (lineSplit.Length == _input.VectorSize)
                {
                    if (IsValidInput(lineSplit))
                    {
                        ParseInput(lineSplit);
                    }
                    else
                    {
                        MessageBox.Show("Coudn't parse vector: Test Vector should consist of integers");
                    }
                }
                else
                {
                    MessageBox.Show("Your vector should be of length "+_input.VectorSize+" instead of "+ lineSplit.Length);
                }
            }
            else
            {
                MessageBox.Show("Please try with a valid vector again");
            }
        }
        
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool IsValidInput(string[] elements)
        {
            int val;
            foreach(var elem in elements)
            {
                if (!int.TryParse(elem, out val))
                    return false;
            }
            return true;
        }

        private void ParseInput(string[] lineSplit)
        {
            foreach (var v in lineSplit)
            {
                _testVector.Add(int.Parse(v));
            }
            _writer.Write("Test Vector");
            _writer.Write(_testVector);
            var result = _associativeMemory.Test(_testVector);
            _writer.Write(result);
            ConsoleText = _writer.Text;
        }
    }
}
