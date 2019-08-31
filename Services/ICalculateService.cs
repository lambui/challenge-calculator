using System.Collections.Generic;

namespace restaurantCalculator {
    public interface ICalculateService {
        int UpperBound {get; set;}
        bool AllowNegative {get; set;}
        string Sum(List<int> numbers);
    }
}