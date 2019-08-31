using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace restaurantCalculator.Tests
{
    [TestClass]
    public class DelimiterServiceTests
    {
        private DelimiterService _deliService = new DelimiterService();
        private string delimStartString = "//";

        [TestMethod]
        public void GetDelimiters_SingleCharDelimiter_OneElementList() {
            string input = delimStartString + "|";
            Assert.IsTrue(_deliService.GetDelimiters(input).Count == 1);
        }

        [TestMethod]
        public void GetDelimiters_MultipleCharDelimiterWithoutBracket_ThrowException() {
            Assert.ThrowsException<Exception>(() => {
                string input = delimStartString + "|||";
                _deliService.GetDelimiters(input);
            });
        }

        [TestMethod]
        public void GetDelimiters_MultipleCharDelimiter_MultipleElementList() {
            string input = delimStartString;
            string delim1 = "delim1";
            string delim2 = "!#@#$&@!";
            input = $"{delimStartString}[{delim1}][{delim2}]";
            Assert.IsTrue(_deliService.GetDelimiters(input).Count == 2);
            Assert.IsTrue(_deliService.GetDelimiters(input).Contains(delim1));
            Assert.IsTrue(_deliService.GetDelimiters(input).Contains(delim2));
        }

        [TestMethod]
        public void GetDelimiters_MultipleCharDelimiterMalform_ThrowException() {
            Assert.ThrowsException<Exception>(() => {
                string input = delimStartString;
                string delim1 = "delim1";
                string delim2 = "!#@#$&@!";
                input = $"{delimStartString}[{delim1}[{delim2}]";
                _deliService.GetDelimiters(input);
            });
        }

        [TestMethod]
        public void GetCustomDelimiterString_StringWithCustomDelimiterData_DelimiterString() {
            string input = $"{delimStartString};\\n123;456;789";
            string expected = $"{delimStartString};";
            Assert.IsTrue(_deliService.GetCustomDelimiterString(input).Equals(expected));
        }

        [TestMethod]
        public void GetCustomDelimiterString_StringWithoutCustomDelimiterData_EmptyString() {
            string input = "123;456;789,123";
            string expected = "";
            Assert.IsTrue(_deliService.GetCustomDelimiterString(input).Equals(expected));
        }

        [TestMethod]
        public void GetCalculationString_StringWithCustomDelimiterData_CalculationString() {
            string input = $"{delimStartString};\\n123;456;789";
            string expected = "123;456;789";
            Assert.IsTrue(_deliService.GetCalculationString(input).Equals(expected));
        }

        [TestMethod]
        public void GetCalculationString_StringWithoutCustomDelimiterData_SameString() {
            string input = "123;456;789,123";
            Assert.IsTrue(_deliService.GetCalculationString(input).Equals(input));
        }

        [TestMethod]
        public void FormatCalculationString_NoCustomDelimeters_StringWithOnlyMainDelimeter() {
            string input = "123;456;789,123";
            string expected = input.Replace(_deliService.AlternateDelimiter, _deliService.MainDelimiter);
            Assert.IsTrue(_deliService.FormatCalculationString(input, new List<string>()).Equals(input));
        }

        [TestMethod]
        public void FormatCalculationString_MultipleCustomDelimeters_StringWithOnlyMainDelimeter() {
            string delim1 = "---";
            string delim2 = "!@#";
            string delim3 = "ABC";
            List<string> customDelim = new List<string>();
            customDelim.Add(delim1);
            customDelim.Add(delim2);
            customDelim.Add(delim3);

            string input = $"123{delim1}456{delim2}789{delim3}123";
            string expected = $"123{_deliService.MainDelimiter}456{_deliService.MainDelimiter}789{_deliService.MainDelimiter}123";
            Assert.IsTrue(_deliService.FormatCalculationString(input, new List<string>()).Equals(input));
        }
    }
}
