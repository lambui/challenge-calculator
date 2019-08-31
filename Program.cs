﻿using System;
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