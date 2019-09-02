using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace restaurantCalculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Calculate_SimpleStringInput_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService() {
                AllowNegative = true,
                UpperBound = 1000
            };
            Calculator cal = new Calculator(new DelimiterService(), new ParseService(), calService);
            cal.SetStringSequence("1,20");
            int[] numbers = { 1, 20 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_SimpleStringInputInvalidValue_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService();
            Calculator cal = new Calculator(new DelimiterService(), new ParseService(), calService);
            cal.SetStringSequence("5,tytyt");
            int[] numbers = { 5, 0 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_SimpleStringInputMoreThanTwoValues_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService();
            Calculator cal = new Calculator(new DelimiterService(), new ParseService(), calService);
            cal.SetStringSequence("5,10,15");
            int[] numbers = { 5, 10, 15 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_SimpleStringInputWithAlternateDelimiter_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService();
            Calculator cal = new Calculator(
                new DelimiterService() {
                    AlternateDelimiter = "\\n"
                }, 
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("1\\n2,3");
            int[] numbers = { 1, 2, 3 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_DontAllowNegativeWithNegativeValue_Add_ThrowsException() {
            ICalculateService calService = new CalculateService();
            Calculator cal = new Calculator(
                new DelimiterService(),
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("1,-2,3");
            int[] numbers = { 1, -2, -3 };

            Assert.ThrowsException<Exception>(() => {
                cal.Calculate(Calculator.CalculateOperation.ADD)
                    .Equals(calService.Sum(numbers.ToList()));
                }, 
                "Negative number(s) provided: -2 -3"
            );
        }

        [TestMethod]
        public void Calculate_IgnoreLargeValue_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService();
            Calculator cal = new Calculator(
                new DelimiterService(),
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("2,1001,6");
            int[] numbers = { 2, 0, 6 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_SingleCharCustomDelimiter_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService();
            Calculator cal = new Calculator(
                new DelimiterService(),
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("//;\\n2;5");
            int[] numbers = { 2, 5 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_MultipleCharCustomDelimiter_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService();
            Calculator cal = new Calculator(
                new DelimiterService(),
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("//[***]\\n11***22***33");
            int[] numbers = { 11, 22, 33 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_MultipleCustomDelimiters_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService();
            Calculator cal = new Calculator(
                new DelimiterService(),
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("//[*][!!][rrr]\\n11rrr22*33!!44");
            int[] numbers = { 11, 22, 33, 44 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_CustomAlternateDelimiter_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService();
            Calculator cal = new Calculator(
                new DelimiterService() {
                    AlternateDelimiter = "---"
                },
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("11---33---100");
            int[] numbers = { 11, 33, 100 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_AllowNegativeWithNegativeValue_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService() {
                AllowNegative = true
            };
            Calculator cal = new Calculator(
                new DelimiterService(),
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("11,-3,100");
            int[] numbers = { 11, -3, 100 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_CustomUpperBound_Add_CorrectOutput() {
            ICalculateService calService = new CalculateService() {
                UpperBound = 2000
            };
            Calculator cal = new Calculator(
                new DelimiterService(),
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("11,3,1500,2000,2001");
            int[] numbers = { 11, 3, 1500, 2000, 0 };

            string expectedValue = numbers.Sum().ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.ADD);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Sum(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_Subtract_CorrectOutput() {
            ICalculateService calService = new CalculateService() {
                AllowNegative = true,
                UpperBound = 2000
            };
            Calculator cal = new Calculator(
                new DelimiterService(),
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("//[*][!!][rrr]\\n11rrr22*-33!!44");
            int[] numbers = { 11, 22, -33, 44 };

            string expectedValue = (11-22-(-33)-44).ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.SUBSTRACT);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Subtract(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_Multiply_CorrectOutput() {
            ICalculateService calService = new CalculateService() {
                AllowNegative = true,
                UpperBound = 2000
            };
            Calculator cal = new Calculator(
                new DelimiterService(),
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("//[*][!!][rrr]\\n11rrr22*-33!!44");
            int[] numbers = { 11, 22, -33, 44 };

            string expectedValue = (11*22*-33*44).ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.MULTIPLY);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Multiply(numbers.ToList())));
        }

        [TestMethod]
        public void Calculate_Divine_CorrectOutput() {
            ICalculateService calService = new CalculateService() {
                AllowNegative = true,
                UpperBound = 2000
            };
            Calculator cal = new Calculator(
                new DelimiterService(),
                new ParseService(), 
                calService
            );
            cal.SetStringSequence("//[*][!!][rrr]\\n11rrr22*-33!!44");
            int[] numbers = { 11, 22, -33, 44 };

            string expectedValue = (11/22/-33/44).ToString();
            string value = cal.Calculate(Calculator.CalculateOperation.DIVIDE);
            Assert.IsTrue(value.Contains(expectedValue));
            Assert.IsTrue(value.Equals(calService.Divide(numbers.ToList())));
        }
    }
}
