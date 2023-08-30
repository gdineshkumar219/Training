int number = GetValidNumberInput ();
Console.WriteLine ($"\nWords representation: {NumberToWords (number)}");
Console.WriteLine ($"\nRoman numeral representation: {NumberToRoman (number)}");

int GetValidNumberInput () {
   while (true) {
      Console.Write ("Enter the Number between 1 and 1000: ");
      if (int.TryParse (Console.ReadLine (), out int number) && number >= 1 && number <= 1000) return number;
      Console.WriteLine ("Please enter a valid number between 1 and 1000.");
   }
}

string NumberToWords (int number) {
   string[] unitsPlace = { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
   string[] tensCount = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
   if (number < 20) return unitsPlace[number - 1];
   if (number < 100) {
      int tensDigit = number % 10;
      return tensCount[number / 10] + (tensDigit > 0 ? " " + unitsPlace[(tensDigit) - 1] : "");
   }
   if (number < 1000) {
      int hundredsDigit = number % 100;
      return $"{unitsPlace[number / 100 - 1]} Hundred" + (hundredsDigit > 0 ? " and " + NumberToWords (hundredsDigit) : "");
   }
   int thousandsDigit = number % 1000;
   return $"{NumberToWords (number / 1000)} Thousand" + (thousandsDigit > 0 ? " " + NumberToWords (thousandsDigit) : "");
}

string NumberToRoman (int number) {
   string[] symbols = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
   int[] val = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
   string roman = "";
   for (int index = val.Length - 1; index >= 0; index--)
      while (number >= val[index]) {
         roman += symbols[index];
         number -= val[index];
      }
   return roman;
}