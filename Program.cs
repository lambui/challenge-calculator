using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator cal = new Calculator(new DelimiterService(), new ParseService(), new CalculateService());
            while(true) { // Allow the application to process entered entries until Ctrl+C is used
                Console.Write("Configure Alternate Delimiter: ");
                string alDelim = Console.ReadLine();

                Console.Write("Allow negative values (y/n): ");
                string allowNegative = Console.ReadLine();

                Console.Write("Set upper bound: ");
                string upperBound = Console.ReadLine();

                Console.Write("Enter string: ");
                string input = Console.ReadLine();

                cal .SetAlternateDelimiter(string.IsNullOrEmpty(alDelim) ? "\\n" : alDelim)
                    .AllowNegative(allowNegative.ToLower() == "y" ? true : false)
                    .SetUpperBound(string.IsNullOrEmpty(upperBound) ? 1000 : int.Parse(upperBound))
                    .SetStringSequence(input)
                    .Calculate(Calculator.CalculateOperation.ADD)
                    .Calculate(Calculator.CalculateOperation.SUBSTRACT)
                    .Calculate(Calculator.CalculateOperation.MULTIPLY)
                    .Calculate(Calculator.CalculateOperation.DIVINE);
            }
        }
    }    
}
