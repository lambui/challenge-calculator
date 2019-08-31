# String Calculator Challenge

## Assume: 
 - input numbers are integer, any other numbers (like float) are invalid numbers. This is because if delimiter is a period, it can cause problem and there are no clear indication on "number" having to include float.
 - output number can be float, since there are division.

## Structure:
 - Main project code is in <b>restaurantCalculator</b>
 - Unit test code is in <b>restaurantCalculator.Tests</b>

## Run:
### Use dotnet cli:
 - Main project:  
   1. go to <b>restaurantCalculator</b>
   1. dotnet run
      - <b>Configure Alternate Delimiter</b>: set custom alternate delimiter here, or press enter pass it to use default value (\n)
      - <b>Allow negative values (y/n)</b>: toggle to use negative value or not. Any other input than 'y' will use default value (not allow negative values)
      - <b>Set upper bound</b>: set upperbound value here, or press enter pass it to use default value (1000)
      - Then input the string for calculation. Everytime the calculation done, the loop repeats to allow user to set different configuration and input value.
      - <b>Ctrl-C</b> to end
 - Unit tests:
   1. go to <b>restaurantCalculator.Tests</b>
   2. dotnet test


