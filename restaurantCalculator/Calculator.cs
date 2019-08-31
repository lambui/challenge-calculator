using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator {
    public class Calculator {
        public enum CalculateOperation {
            ADD, SUBSTRACT, MULTIPLY, DIVINE
        }
        public string stringSequence {get; private set;} = "";
        private List<int> numberSequence;

        private IDelimiterService _deliService;
        private IParseService _parseService;
        private ICalculateService _calService;

        public Calculator(IDelimiterService deliService, IParseService parseService, ICalculateService calService) {
            _deliService = deliService;
            _parseService = parseService;
            _calService = calService;
        }

        public Calculator SetAlternateDelimiter(string input) {
            _deliService.AlternateDelimiter = input;
            return this;
        }

        public Calculator AllowNegative(bool allow) {
            _calService.AllowNegative = allow;
            return this;
        }

        public Calculator SetUpperBound(int upperBound) {
            _calService.UpperBound = upperBound;
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
        
        public string Calculate(CalculateOperation operation) {
            // calculate sum
            switch (operation) {
                default: // default is ADD
                case CalculateOperation.ADD: 
                    return _calService.Sum(numberSequence);
                case CalculateOperation.SUBSTRACT: 
                    return _calService.Subtract(numberSequence);
                case CalculateOperation.MULTIPLY: 
                    return _calService.Multiply(numberSequence);
                case CalculateOperation.DIVINE: 
                    return _calService.Divine(numberSequence);
            }
        }
    }
}