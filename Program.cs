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
   string[] units = { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
   string[] teens = { "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
   string[] tensCount = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
   if (number < 10) return units[number - 1];
   if (number < 20) return teens[number - 11];
   if (number < 100) return tensCount[number / 10] + (number % 10 > 0 ? " " + units[number % 10 - 1] : "");
   if (number < 1000) return $"{units[number / 100 - 1]} Hundred" + (number % 100 > 0 ? " and " + NumberToWords (number % 100) : "");
   return $"{NumberToWords (number / 1000)} Thousand" + (number % 1000 > 0 ? " " + NumberToWords (number % 1000) : "");
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