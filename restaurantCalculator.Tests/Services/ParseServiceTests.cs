using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace restaurantCalculator.Tests
{
    [TestClass]
    public class ParseServiceTests {
        private ParseService _parseService = new ParseService();

        [TestMethod]
        public void GetNumberSequence_InputStringContainsOnlyValidInt_ListOfAllValidNumbers() {
            List<int> expected = new List<int>();
            expected.Add(123);
            expected.Add(1000000);
            expected.Add(-10);
            expected.Add(0);
            string delimiter = ",";
            string input = $"{expected[0]}{delimiter}{expected[1]}{delimiter}{expected[2]}{delimiter}{expected[3]}";

            List<int> list = _parseService.GetNumberSequence(input, delimiter);
            Assert.IsTrue(list.Count == expected.Count);
            expected.ForEach(item => Assert.IsTrue(list.Contains(item)));
        }

        [TestMethod]
        public void GetNumberSequence_InputStringContainsInvalidElement_ListOfNumbersWithInvalidBeingZero() {
            List<int> expected = new List<int>();
            expected.Add(123);
            expected.Add(1000000);
            expected.Add(-10);
            expected.Add(0);
            List<object> invalid = new List<object>();
            invalid.Add(true);
            invalid.Add("hello world");
            invalid.Add("-123-");
            invalid.Add("123abc"); 
            string delimiter = ",";
            string input = $"{expected[0]}{delimiter}{expected[1]}{delimiter}{expected[2]}{delimiter}{expected[3]}";
            input += $"{delimiter}{invalid[0]}{delimiter}{invalid[1]}{delimiter}{invalid[2]}{delimiter}{invalid[3]}";

            List<int> list = _parseService.GetNumberSequence(input, delimiter);
            Assert.IsTrue(list.Count == expected.Count + invalid.Count);
            for (int i = 0; i < list.Count; i++) {
                if (i < 4) {
                    Assert.IsTrue(list[i] == expected[i]);
                } else {
                    Assert.IsTrue(list[i] == 0);
                }
            }
        }
    }
}