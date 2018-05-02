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

namespace NNProject1.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Input input;
        AssociativeMemory associativeMemory;
        List<int> testVector;
        
        public MainWindow()
        {
            InitializeComponent();
            AppendToConsole("Welcome to our Neural Networks Project");
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    input = new Input(openFileDialog.OpenFile());
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error creating input class" + ex.Message);
                }
                finally
                {
                    if(input != null)
                    {
                        int index = 0;
                        foreach (List<int> l in input.Values)
                        {
                            AppendToConsole("Vector " + index);
                            AppendListToConsole(l);
                            index++;
                        }
                        CreateAssociativeMemoryButton.IsEnabled = true;
                    }
                }
            }
        }

        private void CreateAssociativeMemoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                associativeMemory = new AssociativeMemory(input.Values, input.numberOfVectors, input.vectorSize);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error creating associative memory" + ex.Message);
            }
            finally
            {
                if(associativeMemory != null)
                {
                    AppendToConsole("Associative memory created");
                    InputTextBox.IsEnabled = true;
                    TestVectorButton.IsEnabled = true;
                    AppendToConsole("Hopfield Matrix");
                    AppendToConsole(associativeMemory.T.ToString());
                    AppendToConsole("Now you can input a test vector of length " + input.vectorSize);
                    InputTextBox.Clear();
                }
                
            }
        }

        private void TestVectorButton_Click(object sender, RoutedEventArgs e)
        {
            testVector = new List<int>();
            string line = InputTextBox.Text;
            char[] charSeparators = new char[] { ',', ' ' };
            if (line != null)
            {
                string[] lineSplit = line.Split(charSeparators,StringSplitOptions.RemoveEmptyEntries);
                if (lineSplit.Length == input.vectorSize)
                {
                    foreach(var v in lineSplit)
                    {
                        testVector.Add(Int32.Parse(v));
                    }
                    AppendToConsole("Test Vector");
                    AppendListToConsole(testVector);
                    var result = associativeMemory.Test(testVector);
                    AppendToConsole("Result after testing");
                    AppendListToConsole(result.Result);
                }
                else
                {
                    MessageBox.Show("Your vector should be of length "+input.vectorSize+" instead of "+ lineSplit.Length);
                }
            }
            else
            {
                MessageBox.Show("Please try with a valid vector again");
            }

        }

        private void AppendFreeLineToConsole()
        {
            textBox.AppendText("\n");
        }

        private void AppendToConsole(string text)
        {
            textBox.AppendText(text);
            AppendFreeLineToConsole();
            AppendFreeLineToConsole();
            textBox.ScrollToEnd();
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

    }
}
