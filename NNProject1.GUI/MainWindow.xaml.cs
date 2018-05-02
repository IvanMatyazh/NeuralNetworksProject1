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
        List<List<int>> input;
        int vectorSize;
        int numberOfVectors;
        bool isHeader;
        AssociativeMemory associativeMemory;
        List<int> testVector;
        char[] charSeparators = new char[] { ',', ' ' };


        public MainWindow()
        {
            InitializeComponent();
            
            isHeader = true;
            AppendToConsole("Welcome to our Neural Networks Project");


        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            input = new List<List<int>>();
            vectorSize = 0;
            numberOfVectors = 0;
            String line;
            StreamReader myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            int currentVector = 0;
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if ((myStream = new StreamReader(openFileDialog.OpenFile())) != null)
                    {
                        using (myStream)
                        {
                            line = myStream.ReadLine();// Reading the header with the size of each vector
                            vectorSize = Int32.Parse(line);
                            isHeader = false;
                            
                            while ((((line = myStream.ReadLine()) != null)) && isHeader == false)
                            {

                                string[] lineSplit = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                                if (lineSplit.Length == vectorSize)
                                {
                                    List<int> tempVector = new List<int>(vectorSize);
                                    foreach(var elem in lineSplit)
                                    {
                                        tempVector.Add(Int32.Parse(elem));
                                    }
                                    input.Add(tempVector);
                                    AppendToConsole("Vector " + currentVector);
                                    AppendListToConsole(tempVector);
                                    currentVector++;
                                }
                                else
                                {
                                    MessageBox.Show("The vector at line: " + (currentVector + 1) + " should be of length: " + vectorSize + " instead of " + lineSplit.Length);
                                    myStream.Close();
                                    currentVector = 0;
                                }   
                            }
                        }
                    }
                    numberOfVectors = currentVector;
                    myStream.Close();
                    CreateAssociativeMemoryButton.IsEnabled = true;
                    AppendToConsole(numberOfVectors + " vectors parsed with " + vectorSize + " elements each");
                    
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        private void CreateAssociativeMemoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                associativeMemory = new AssociativeMemory(input, numberOfVectors, vectorSize);

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error creating associative memory" + ex.Message);
            }
            finally
            {
                MessageBox.Show("Associative memory created");
                InputTextBox.IsEnabled = true;
                TestVectorButton.IsEnabled = true;

                AppendToConsole("Hopfield Matrix");
                AppendToConsole(associativeMemory.T.ToString());
                AppendToConsole("Now you can input a test vector of length "+ vectorSize);
                InputTextBox.Clear();
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

        private void TestVectorButton_Click(object sender, RoutedEventArgs e)
        {
            testVector = new List<int>();
            string line = InputTextBox.Text;
            
            if (line != null)
            {
                string[] lineSplit = line.Split(charSeparators,StringSplitOptions.RemoveEmptyEntries);
                if (lineSplit.Length == vectorSize)
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
                    MessageBox.Show("Your vector should be of length "+vectorSize+" instead of "+ lineSplit.Length);
                }
            }
            else
            {
                MessageBox.Show("Please try with a valid vector again");
            }

        }

    }
}
