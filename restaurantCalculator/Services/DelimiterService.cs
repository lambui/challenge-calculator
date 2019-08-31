using System;
using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator {
    public class DelimiterService : IDelimiterService {
        public string MainDelimiter {get; private set;} = ",";

        public string AlternateDelimiter {get; set;} = "\\n";

        /// <summary>
        /// Extract list of custom delimiters from string with following format:
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
        public List<string> GetDelimiters(string input) {
            input = input.Remove(0, "//".Length);
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

        /// <summary>
        /// Extract string defining custom delimiters from input string sequence.
        /// </summary>
        /// <param name="input">whole input string sequence</param>
        /// <returns>part of the string that defines custom delimiters</returns>
        public string GetCustomDelimiterString(string input) {
            if (input.StartsWith("//")) {
                string[] inputParts = input.Split("\\n");
                if (inputParts.Length > 1) {
                    return inputParts[0];
                }
            }
            return "";
        }

        /// <summary>
        /// Extract string defining the calculation part from input string sequence.
        /// 
        /// (The other part of the input string besides custom delimiter part)
        /// </summary>
        /// <param name="input">whole input string sequence</param>
        /// <returns>part of the string that defines calculation to be processed</returns>
        public string GetCalculationString(string input) {
            string customDelimiterString = GetCustomDelimiterString(input);
            if (string.IsNullOrEmpty(customDelimiterString)) {
                return input;
            } else {
                return input.Substring($"{customDelimiterString}\\n".Length);
            }
        }

        /// <summary>
        /// Format calculation string to convert all different kinds of delimiters into main delimiter.
        /// </summary>
        /// <param name="input">calculation string</param>
        /// <param name="customDelimiters">list of custom delimiters</param>
        /// <returns>calculation string with only main delimiter</returns>
        public string FormatCalculationString(string input, List<string> customDelimiters) {
            customDelimiters.ForEach(item => {
                input = input.Replace(item, MainDelimiter);
            });
            input = input.Replace(AlternateDelimiter, MainDelimiter);
            return input;
        }
    }
}