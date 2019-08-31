using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator {
    public class Calculator {
        public enum CalculateOperation {
            ADD, SUBSTRACT, MULTIPLY, DIVISION
        }
        public string stringSequence {get; private set;} = "";
        private List<int> numberSequence;

        private IDelimiterService _deliService;
        private IParseService _parseService;

        public Calculator(IDelimiterService deliService, IParseService parseService) {
            _deliService = deliService;
            _parseService = parseService;
        }

        public Calculator SetAlternateDelimiter(string input) {
            _deliService.AlternateDelimiter = input;
            return this;
        }

        public Calculator SetStringSequence(string input) {
            stringSequence = input;
            ProcessStringSequence();
            return this;
        }

        /// <summary>
        /// Process string sequence to extract sequence of numbers to perform calculate on.
        /// 
        /// Will populate numberSequence variable based on stringSequence variable
        /// </summary>
        private void ProcessStringSequence() {
            string input = string.Copy(stringSequence);
            
            List<string> customDelimiters = GetCustomDelimiters();
            if (customDelimiters.Count > 0) {
                input = _deliService.GetCalculationString(input);
            }
            
            // format input
            input = _deliService.FormatCalculationString(input, customDelimiters);

            // parse string into numbers to perform operation
            numberSequence = _parseService.GetNumberSequence(input, _deliService.MainDelimiter);
        }

        /// <summary>
        /// Extract custom delimiters from stringSequence variable.
        /// </summary>
        /// <returns>list of custom delimiters</returns>
        private List<string> GetCustomDelimiters() {
            // parse custom delimeter
            List<string> delimiterList = new List<string>();
            string delimiterString = _deliService.GetCustomDelimiterString(stringSequence);
            if (string.IsNullOrEmpty(delimiterString) == false) {
                delimiterList = _deliService.GetDelimiters(delimiterString);
            }
            return delimiterList;
        }

        /// <summary>
        /// Get list of negative numbers from list of numbers.
        /// </summary>
        /// <param name="numberList">list of numbers</param>
        /// <returns>list containing negative numbers</returns>
        private List<int> GetNegativeNumberList(List<int> numberList) {
            return numberList.Where(item => item < 0).ToList();
        }

        private string Sum(List<int> numberList) {
            int sum = 0; 
            string sumEquation = "";
            numberList.ForEach(item => {
                sumEquation += $"{item}+";
                sum+=item;
            });
            sumEquation = $"{sumEquation.Remove(sumEquation.Length-1, 1)} = {sum}";
            return sumEquation;
        }
        
        public string Calculate(CalculateOperation operation) {
            // throw error for every negative numbers
            List<int> negative = GetNegativeNumberList(numberSequence);
            if (negative.Count > 0) {
                string error = "Negative number(s) provided: ";
                negative.ForEach(item => error += item + " "); 
                throw new Exception(error);
            }

            // calculate sum
            switch (operation) {
                case CalculateOperation.ADD: return Sum(numberSequence);
                default: return Sum(numberSequence);
            }
        }
    }
}