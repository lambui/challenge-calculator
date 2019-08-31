using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter string: ");
            string input = Console.ReadLine();

            // parse custom delimeter
            string customDelimeter = "";
            if (input.StartsWith("//")) {
                List<string> inputParts = input.Split("\\n").ToList();
                string inputDelimeter = "";
                if (inputParts.Count > 1) {
                    inputDelimeter = inputParts[0];
                    input = input.Remove(0, inputDelimeter.Length + "\\n".Length);
                }
                customDelimeter = inputDelimeter.Remove(0, "//".Length);
                if (customDelimeter.Length > 1) {
                    throw new Exception("Custom delimeter length is too long.");
                }
            }

            // format input
            input = input.Replace(customDelimeter, ","); // apply custom delimeter
            input = input.Replace("\\n", ","); // support newline (literal \n) as delimeter
            List<string> numberStrings = input.Split(",").ToList();

            // parse string into numbers to perform operation
            List<int> numberList = new List<int>();
            List<int> negative = new List<int>();
            numberStrings.ForEach(item => {
                if (int.TryParse(item.Trim(), out int number) == false) { // deny invalid number, including float
                    return;
                }
                if (number < 0) { // deny negative number
                    negative.Add(number);
                    return;
                }
                if (number > 1000) { // ignore number > 1000
                    return;
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
            numberList.ForEach(item => sum+=item);
            Console.WriteLine(sum);
        }
    }
}
