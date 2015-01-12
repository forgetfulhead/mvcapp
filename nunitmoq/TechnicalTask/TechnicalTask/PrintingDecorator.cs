using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalTask
{

    /// <summary>
    /// Any class implementing this interface could be swapped
    /// with the ConsolePrintingDecorator class below and used to print to 
    /// another device eg flat file or XML
    /// </summary>
    public interface IPrintingDecorator
    {
        string Print(string text);
    }

    /// <summary>
    /// Prints any string passed to the Print method to the Console
    /// </summary>
    class ConsolePrintingDecorator :IPrintingDecorator
    {
        public string Print(string text)
        {
            Console.WriteLine(text);
            return text;
        }
    }
}
