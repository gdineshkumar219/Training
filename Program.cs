int decimalNumber = GetIntegerInput ("Enter a decimal number:");
string binary = Convert.ToString (decimalNumber, 2);
string hexadecimal = decimalNumber.ToString ("X");
Console.WriteLine ($"Binary value of {decimalNumber}: {binary}");
Console.WriteLine ($"Hexadecimal value of {decimalNumber}: {hexadecimal}");
int GetIntegerInput (string prompt) {
   while (true) {
      Console.Write (prompt);
      if (int.TryParse (Console.ReadLine (), out int value)) return value;
      Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}