using System.Collections.Generic;

namespace restaurantCalculator {
    public interface IParseService {
        List<int> GetNumberSequence(string input, string delimiter);
    }
}