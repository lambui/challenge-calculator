using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace restaurantCalculator.Tests
{
    [TestClass]
    public class CalculateServiceTests {
        private CalculateService _calService = new CalculateService();
        private int[] input = { 10, 20, 30, -10, 1000, 2000 };

        [TestMethod]
        public void Sum_AllowNegative_UpperBound1000_CorrectOutput() {
            List<int> list = input.ToList();
            _calService.AllowNegative = true;
            _calService.UpperBound = 1000;
            float expectedValue = 0;
            string expected = "";
            list.ForEach(item => {
                if (item > 1000) {
                    expected += "0+";
                } else {
                    expectedValue += item;
                    expected += $"{item}+";
                }
            });
            expected = $"sum: {expected.Remove(expected.Length-1, 1)} = {expectedValue}";
            Assert.IsTrue(_calService.Sum(list).Equals(expected));
        }

        [TestMethod]
        public void Sum_DontAllowNegative_UpperBound1000_ThrowsException() {
            _calService.AllowNegative = false;
            _calService.UpperBound = 1000;
            List<int> list = input.ToList();
            Assert.ThrowsException<Exception>(() => {
                _calService.Sum(list);
            });
        }

        [TestMethod]
        public void Subtract_AllowNegative_UpperBound1000_CorrectOutput() {
            List<int> list = input.ToList();
            _calService.AllowNegative = true;
            _calService.UpperBound = 1000;
            float expectedValue = 0;
            string expected = "";
            for (int i = 0; i < list.Count; i++) {
                int item = list[i];
                if (i == 0) {
                    expectedValue = item;
                    expected = $"{item}-";
                    continue;
                }
                if (item > 1000) {
                    expected += "0-";
                } else {
                    expectedValue -= item;
                    expected += $"{item}-";
                }
            }
            expected = $"subtract: {expected.Remove(expected.Length-1, 1)} = {expectedValue}";
            Assert.IsTrue(_calService.Subtract(list).Equals(expected));
        }

        [TestMethod]
        public void Subtract_DontAllowNegative_UpperBound1000_ThrowsException() {
            _calService.AllowNegative = false;
            _calService.UpperBound = 1000;
            List<int> list = input.ToList();
            Assert.ThrowsException<Exception>(() => {
                _calService.Subtract(list);
            });
        }

        [TestMethod]
        public void Multiply_AllowNegative_UpperBound1000_CorrectOutput() {
            List<int> list = input.ToList();
            _calService.AllowNegative = true;
            _calService.UpperBound = 1000;
            float expectedValue = 0;
            string expected = "";
            for (int i = 0; i < list.Count; i++) {
                int item = list[i];
                if (i == 0) {
                    expectedValue = item;
                    expected = $"{item}*";
                    continue;
                }
                if (item > 1000) {
                    expected += "1*";
                } else {
                    expectedValue *= item;
                    expected += $"{item}*";
                }
            }
            expected = $"multiply: {expected.Remove(expected.Length-1, 1)} = {expectedValue}";
            Assert.IsTrue(_calService.Multiply(list).Equals(expected));
        }

        [TestMethod]
        public void Multiply_DontAllowNegative_UpperBound1000_ThrowsException() {
            _calService.AllowNegative = false;
            _calService.UpperBound = 1000;
            List<int> list = input.ToList();
            Assert.ThrowsException<Exception>(() => {
                _calService.Multiply(list);
            });
        }

        [TestMethod]
        public void Divine_AllowNegative_UpperBound1000_CorrectOutput() {
            List<int> list = input.ToList();
            _calService.AllowNegative = true;
            _calService.UpperBound = 1000;
            float expectedValue = 0;
            string expected = "";
            for (int i = 0; i < list.Count; i++) {
                int item = list[i];
                if (i == 0) {
                    expectedValue = item;
                    expected = $"{item}/";
                    continue;
                }
                if (item > 1000) {
                    expected += "1/";
                } else {
                    expectedValue /= item;
                    expected += $"{item}/";
                }
            }
            expected = $"divine: {expected.Remove(expected.Length-1, 1)} = {expectedValue}";
            Assert.IsTrue(_calService.Divine(list).Equals(expected));
        }

        [TestMethod]
        public void Divine_DontAllowNegative_UpperBound1000_ThrowsException() {
            _calService.AllowNegative = false;
            _calService.UpperBound = 1000;
            List<int> list = input.ToList();
            Assert.ThrowsException<Exception>(() => {
                _calService.Divine(list);
            });
        }
    }
}