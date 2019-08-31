using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator cal = new Calculator(new DelimiterService(), new ParseService())
                                .SetAlternateDelimiter("\\n");
            while(true) { // Allow the application to process entered entries until Ctrl+C is used
                Console.Write("Enter string: ");
                string input = Console.ReadLine();

                string output = cal .SetStringSequence(input)
                                    .Calculate(Calculator.CalculateOperation.ADD);

                Console.WriteLine(output);
            }
        }
    }    
}
