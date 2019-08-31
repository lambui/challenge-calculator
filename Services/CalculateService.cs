using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator {
    public class CalculateService : ICalculateService {
        public int UpperBound {get; set;} = 1000;
        public bool AllowNegative {get; set;} = false;

        private string Calculate(List<int> numbers, string operation) {
            numbers = ProcessNumberList(numbers);
            float result = 0; 
            string equation = "";

            // calculate
            for (int i = 0; i < numbers.Count; i++) {
                equation += $"{numbers[i]}{operation}";
                if (i == 0) {
                    result = numbers[i];
                } else {
                    switch(operation) {
                        default: // default is sum
                        case "+": result+=numbers[i]; break;
                        case "-": result-=numbers[i]; break;
                        case "*": result*=numbers[i]; break;
                        case "/": result/=numbers[i]; break;
                    }
                }
            }

            // create report string
            string operationName = "";
            switch(operation) {
                default: // default is sum
                case "+": operationName="sum: "; break;
                case "-": operationName="substract: "; break;
                case "*": operationName="multiply: "; break;
                case "/": operationName="divine: "; break;
            }
            equation = $"{operationName}: {equation.Remove(equation.Length-1, 1)} = {result}";
            return equation;
        }

        public string Sum(List<int> numbers) {
            return Calculate(numbers, "+");
        }

        public string Subtract(List<int> numbers) {
            return Calculate(numbers, "-");
        }

        public string Multiply(List<int> numbers) {
            return Calculate(numbers, "*");
        }

        public string Divine(List<int> numbers) {
            return Calculate(numbers, "/");
        }

        private List<int> NegativeFilter(List<int> numbers) {
            if (AllowNegative == false) {
                // throw error for every negative numbers
                List<int> negative = numbers.Where(item => item < 0).ToList();
                if (negative.Count > 0) {
                    string error = "Negative number(s) provided: ";
                    negative.ForEach(item => error += item + " "); 
                    throw new Exception(error);
                }
            }
            return numbers;
        }

        private List<int> UpperBoundFilter(List<int> numbers) {
            return numbers.Select(item => {
                if (item > UpperBound) {
                    return 0;
                } else {
                    return item;
                }
            }).ToList();
        }

        private List<int> ProcessNumberList(List<int> numbers) {
            numbers = NegativeFilter(numbers);
            numbers = UpperBoundFilter(numbers);            
            return numbers;
        }
    }
}