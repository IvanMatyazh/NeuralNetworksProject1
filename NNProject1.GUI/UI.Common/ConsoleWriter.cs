using NNProject1.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNProject1.GUI.UI.Common
{
    public class ConsoleWriter
    {
        private StringBuilder _builder;

        public string Text
        {
            get
            {
                return _builder.ToString();
            }
        }

        public ConsoleWriter()
        {
            _builder = new StringBuilder();
        }

        public void Write(AssociativeMemory mem)
        {
            if(mem != null)
                _builder.Append(mem.T);
            _builder.Append("\n");
        }
        
        public void Write(string text)
        {
            _builder.Append(text);
            _builder.Append("\n\n");
        } 

        public void Write(List<int> list)
        {
            _builder.Append("[ ");
            foreach (var v in list)
            {
                _builder.Append(v + " ");
            }
            _builder.Append("]\n\n");
        }

        public void Write(TestResult result)
        {
            _builder.Append("Result after testing:\n");
            if (result.IsConvergent)
                WriteConvergentResult(result);
            else if (result.IsTwoVectorsLoop)
                WriteTwoValuesLoopResult(result);
            else
                WriteNonConvergentResult(result);
        }

        public void Write(Input input)
        {
            if (input != null)
            {
                int index = 0;
                foreach (var list in input.Values)
                {
                    Write("Vector " + index);
                    Write(list);
                    index++;
                }
            }
        }

        private void WriteConvergentResult(TestResult result)
        {
            _builder.Append("Convergent after ");
            _builder.Append(result.NumberOfIterations);
            _builder.Append(" iterations to vector\n");
            Write(result.Result);
        }

        private void WriteTwoValuesLoopResult(TestResult result)
        {
            _builder.Append("Two vectors loop was detected after ");
            _builder.Append(result.NumberOfIterations);
            _builder.Append(" iterations\nFirst vector\n");
            Write(result.Result);
            _builder.Append("Second vector\n");
            Write(result.SecondVector);
        }

        private void WriteNonConvergentResult(TestResult result)
        {
            _builder.Append("The vector didn't converge after ");
            _builder.Append(result.NumberOfIterations);
            _builder.Append(" iterations\nLast calculated vector is\n");
            Write(result.Result);
        } 
    }
}
