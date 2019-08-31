using System.Collections.Generic;

namespace restaurantCalculator {
    public interface IDelimiterService {
        string MainDelimiter {get;}
        string AlternateDelimiter {get; set;}
        string GetCustomDelimiterString(string input);
        string GetCalculationString(string input);
        List<string> GetDelimiters(string input);
        string FormatCalculationString(string input, List<string> customDelimiters);
    }
}