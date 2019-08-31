using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true) { // Allow the application to process entered entries until Ctrl+C is used
                Console.Write("Enter string: ");
                string input = Console.ReadLine();

                // parse custom delimeter
                List<string> customDelimiters = new List<string>();
                if (input.StartsWith("//")) {
                    List<string> inputParts = input.Split("\\n").ToList();
                    string inputDelimiter = "";
                    if (inputParts.Count > 1) {
                        inputDelimiter = inputParts[0];
                        input = input.Remove(0, inputDelimiter.Length + "\\n".Length);
                    }
                    customDelimiters = GetDelimiters(inputDelimiter.Remove(0, "//".Length));
                }

                // format input
                customDelimiters.ForEach(item => {
                    input = input.Replace(item, ",");
                });
                input = input.Replace("\\n", ","); // support newline (literal \n) as delimeter
                List<string> numberStrings = input.Split(",").ToList();

                // parse string into numbers to perform operation
                List<int> numberList = new List<int>();
                List<int> negative = new List<int>();
                numberStrings.ForEach(item => {
                    if (int.TryParse(item.Trim(), out int number) == false) { // deny invalid number, including float
                        number = 0;
                    }
                    if (number < 0) { // deny negative number
                        negative.Add(number);
                        return;
                    }
                    if (number > 1000) { // ignore number > 1000
                        number = 0;
                    }
                    numberList.Add(number);
                });

                // throw error for every negative numbers
                if (negative.Count > 0) {
                    string error = "Negative number(s) provided: ";
                    negative.ForEach(item => error += item + " "); 
                    throw new Exception(error);
                }

                // calculate sum
                int sum = 0; 
                string sumEquation = "";
                numberList.ForEach(item => {
                    sumEquation += $"{item}+";
                    sum+=item;
                });
                sumEquation = $"{sumEquation.Remove(sumEquation.Length-1, 1)} = {sum}";
                Console.WriteLine(sumEquation);
            }
        }

        /// <summary>
        /// Extract list of custom delimiters from string with following format
        /// <code>
        /// {single-char-delimiter}
        /// </code>
        /// or
        /// <code>
        /// [{delimiter-1}][{delimiter-2}]...[{delimiter-n}]
        /// </code>
        /// </summary>
        /// <param name="input">string sequence containing custom delimiter data</param>
        /// <returns>list of delimiters</returns>
        public static List<string> GetDelimiters(string input) {
            List<string> delimiters = new List<string>();
            if (input.Length == 1) {
                delimiters.Add(input);
            } else {
                string[] parts = input.Split("[");
                foreach (string part in parts) {
                    if (string.IsNullOrEmpty(part)) {
                        continue;
                    }
                    if (part.EndsWith("]")) {
                        delimiters.Add(part.Remove(part.Length - 1, 1));
                    } else {
                        throw new Exception("Custom delimeter format is incorrect.");
                    }
                }
            }
            return delimiters;
        }
    }    
}
