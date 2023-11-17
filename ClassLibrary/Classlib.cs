// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------
// Program.cs
// Implement custom double.Parse method that takes a string and returns a double.
// ------------------------------------------------------------------------------------------------
namespace ClassLibrary {

   /// <summary>Provides methods for parsing strings into double values</summary>
   public static class DoubleParser {

      #region Public Methods ----------------------------------------
      /// <summary>Tries to parse the specified string into a double value</summary>
      /// <param name="input">The input string to be parsed</param>
      /// <param name="res">When this method returns, contains the double value equivalent of the input string</param>
      /// <returns><c>true</c> if the conversion was successful; otherwise, <c>false</c>.</returns>
      public static bool TryParse (string input, out double res) {
         input = input.Trim ().ToLower ();
         res = 0.0;
         int sign = 1, expSign = 1, exp = 0;
         double fraction = 0.1;
         int step = 0;
         if (string.IsNullOrWhiteSpace (input) || input.StartsWith ('e') || input.EndsWith ('e')
             || input.EndsWith ('+') || input.EndsWith ('-')) return false;
         int i = 0;
         while (i < input.Length) {
            char c = input[i];
            switch (step) {
               case 0:
                  if (c == '+' || c == '-') {
                     sign = c == '-' ? -1 : 1;
                     i++;
                  }
                  step = 1;
                  break;
               case 1:
                  if (char.IsDigit (c)) res = res * 10 + (c - '0');
                  else if (c == '.') step = 2;
                  else if (c == 'e') step = 3;
                  else {
                     res = 0; return false;
                  }
                  i++;
                  break;
               case 2:
                  if (char.IsDigit (c)) {
                     res += (c - '0') * fraction;
                     fraction *= 0.1;
                  } else if (c == 'e') step = 3;
                  else {
                     res = 0; return false;
                  }
                  i++;
                  break;
               case 3:
                  if (char.IsDigit (c)) exp = exp * 10 + (c - '0');
                  else if ((c == '-' || c == '+') && exp == 0) expSign = c == '-' ? -1 : 1;
                  else {
                     res = 0; return false;
                  }
                  i++;
                  break;
            }
         }
         exp *= expSign;
         res *= Math.Pow (10, exp) * sign;
         return true;
      }
      #endregion
   }
}
