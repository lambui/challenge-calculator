using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator {
    public class CalculateService : ICalculateService {
        public int UpperBound {get; set;} = 1000;
        public bool AllowNegative {get; set;} = false;

        public string Sum(List<int> numbers) {
            numbers = ProcessNumberList(numbers);
            int sum = 0; 
            string sumEquation = "";
            numbers.ForEach(item => {
                sumEquation += $"{item}+";
                sum+=item;
            });
            sumEquation = $"{sumEquation.Remove(sumEquation.Length-1, 1)} = {sum}";
            return sumEquation;
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