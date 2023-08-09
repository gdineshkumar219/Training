Console.Write ("Enter the Number: ");
var number = int.Parse (Console.ReadLine ());
Console.WriteLine ($"\nWords representation: {NumberToWords (number)}");
Console.WriteLine ($"\nRoman numeral representation: {NumberToRoman (number)}");   
string NumberToWords (long number) {
   string words = "";
   if (number < 0) return ($"Minus {NumberToWords (Math.Abs (number))}");
   if ((number / 10000000) > 0) {
      words += NumberToWords (number / 10000000) + " crores ";
      number %= 10000000;
   }
   if ((number / 100000) > 0) {
      words += NumberToWords (number / 100000) + " lakhs ";
      number %= 100000;
   }
   if ((number / 1000) > 0) {
      words += NumberToWords (number / 1000) + " thousand ";
      number %= 1000;
   }
   if ((number / 100) > 0) {
      words += NumberToWords (number / 100) + " hundred ";
      number %= 100;
   }
   if (number > 0) {
      if (words != "")
         words += "and ";
      var units = new[] { "Zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
      var tens = new[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
      if (number < 20)
         words += units[number];
      else {
         words += tens[number / 10 - 2];
         if (number % 10 > 0)
            words += " " + units[number % 10];
      }
   }
   return words;
}
string NumberToRoman (int number) {
   if (number == 0) return "No symbol";
   if (number < 0) return ($"Minus {NumberToRoman (Math.Abs (number))}");
   var symbols = new[] { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M", "MV'", "V'", "MX'", "X'" };
   var val = new[] { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000, 4000, 5000, 9000, 10000 };
   string roman = "";
   for (int index = val.Length - 1; index >= 0; index--) {
      while (number >= val[index]) {
         roman += symbols[index];
         number -= val[index];
      }
   }
   return roman;
}