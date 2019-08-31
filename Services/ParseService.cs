using System.Collections.Generic;
using System.Linq;

namespace restaurantCalculator {
    public class ParseService : IParseService {
        /// <summary>
        /// Parse calculation string input into list of number.
        /// </summary>
        /// <param name="input">calculation string to parse</param>
        /// <param name="delimiter">delimiter string to separate numbers in string</param>
        /// <returns>list of number</returns>
        public List<int> GetNumberSequence(string input, string delimiter) {
            List<string> numberStrings = input.Split(delimiter).ToList();
            List<int> numberList = new List<int>();
            numberStrings.ForEach(item => {
                if (int.TryParse(item.Trim(), out int number) == false) { // deny invalid number, including float
                    number = 0;
                }
                numberList.Add(number);
            });
            return numberList;
        }
    }
}