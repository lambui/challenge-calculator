using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator cal = new Calculator(new DelimiterService(), new ParseService(), new CalculateService())
                                .SetAlternateDelimiter("\\n")
                                .AllowNegative(true)
                                .SetUpperBound(2000);
            while(true) { // Allow the application to process entered entries until Ctrl+C is used
                Console.Write("Enter string: ");
                string input = Console.ReadLine();

                cal .SetStringSequence(input)
                    .Calculate(Calculator.CalculateOperation.ADD)
                    .Calculate(Calculator.CalculateOperation.SUBSTRACT)
                    .Calculate(Calculator.CalculateOperation.MULTIPLY)
                    .Calculate(Calculator.CalculateOperation.DIVINE);
            }
        }
    }    
}
