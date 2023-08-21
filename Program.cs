int decimalNumber = GetIntegerInput ();
string binary = Dec2Bin (decimalNumber);
string hex = Dec2Hex (decimalNumber);
Console.WriteLine ($"Hexadecimal value of {decimalNumber}: {hex}");
Console.WriteLine ($"Binary value of {decimalNumber}: {binary}");

int GetIntegerInput () {
   Console.Write ("\nEnter a decimal number:");
   while (true) {
      if (int.TryParse (Console.ReadLine (), out int value)) return value;
      Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}

string Dec2Bin (int num) {
   string binary = "";
   while (num > 0) {
      int remainder = num % 2;
      binary = remainder + binary;
      num /= 2;
   }
   return binary;
}

string Dec2Hex (int num) {
   string hex = "";
   string hexadecimal = "0123456789ABCDEF";
   while (num > 0) {
      int remainder = num % 16;
      hex = hexadecimal[remainder] + hex;
      num /= 16;
   }
   return hex;
}