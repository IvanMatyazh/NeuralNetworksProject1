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

        public RelayCommand OpenFileCommand => _openFileCommand ?? (_openFileCommand = new RelayCommand(OnOpenFileClick));
        public RelayCommand ExitCommand => _exitCommand ?? (_exitCommand = new RelayCommand(OnExitClick));
        public RelayCommand CreateMemoryCommand => _createMemoryCommand ?? (_createMemoryCommand = new RelayCommand(OnCreateMemoryClick, CanExecuteCreateMemoryClick));
        public RelayCommand TestVectorCommand => _testVectorCommand ?? (_testVectorCommand = new RelayCommand(OnTestVectorClick, CanExecuteTestVectorClick));
        #endregion

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            AppendToConsole("Welcome to our Neural Networks Project");
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
                    if(_input != null)
                    {
                        int index = 0;
                        foreach (List<int> l in _input.Values)
                        {
                            AppendToConsole("Vector " + index);
                            AppendListToConsole(l);
                            index++;
                        }
                    }
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
                    AppendToConsole("Associative memory created");
                    AppendToConsole("Hopfield Matrix");
                    AppendToConsole(_associativeMemory.T.ToString());
                    AppendToConsole("Now you can input a test vector of length " + _input.VectorSize);
                    TestVectorText = string.Empty;
                    IsEnabledTestVectorTextBox = true;
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
            string line = TestVectorText;
            char[] charSeparators = new char[] { ',', ' ' };
            if (line != null)
            {
                string[] lineSplit = line.Split(charSeparators,StringSplitOptions.RemoveEmptyEntries);
                if (lineSplit.Length == _input.VectorSize)
                {
                    foreach(var v in lineSplit)
                    {
                        _testVector.Add(int
                            .Parse(v));
                    }
                    AppendToConsole("Test Vector");
                    AppendListToConsole(_testVector);
                    var result = _associativeMemory.Test(_testVector);
                    AppendToConsole("Result after testing");
                    AppendListToConsole(result.Result);
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

        private void AppendFreeLineToConsole()
        {
            ConsoleText += "\n";
        }

        private void AppendToConsole(string text)
        {
            ConsoleText += text;
            AppendFreeLineToConsole();
            AppendFreeLineToConsole();
        }

        private void AppendListToConsole(List<int> list)
        {
            StringBuilder text = new StringBuilder();
            text.Append("[ ");
            foreach (var v in list)
            {
                text.Append(v + " ");
            }
            text.Append("]");
            AppendToConsole(text.ToString());
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
