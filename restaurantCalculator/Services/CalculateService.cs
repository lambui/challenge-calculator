using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator {
    public class CalculateService : ICalculateService {
        public int UpperBound {get; set;} = 1000;
        public bool AllowNegative {get; set;} = false;

        private string Calculate(List<int> numbers, string operation) {
            string operationName = "";
            switch(operation) {
                default: // default is sum
                case "+":
                    numbers = ProcessNumberList(numbers, false);
                    operationName="sum";
                    break;
                case "-":
                    numbers = ProcessNumberList(numbers, false);
                    operationName="subtract";
                    break;
                case "*":
                    numbers = ProcessNumberList(numbers, true);
                    operationName="multiply";
                    break;
                case "/":
                    numbers = ProcessNumberList(numbers, true);
                    operationName="divine";
                    break;
            }
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

        public string Divide(List<int> numbers) {
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

        private List<int> UpperBoundFilter(List<int> numbers, bool forMultiplication) {
            return numbers.Select(item => {
                if (item > UpperBound) {
                    if (forMultiplication) {
                        return 1;
                    } else {
                        return 0;
                    }
                } else {
                    return item;
                }
            }).ToList();
        }

        private List<int> ProcessNumberList(List<int> numbers, bool forMultiplication) {
            numbers = NegativeFilter(numbers);
            numbers = UpperBoundFilter(numbers, forMultiplication);            
            return numbers;
        }
    }
}