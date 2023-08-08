int num1 = GetIntegerInput ("Enter the first number: ");
int num2 = GetIntegerInput ("Enter the second number: ");

int gcd = 1;
for (int i = 1; i <= Math.Min (num1, num2); i++) {
   if (num1 % i == 0 && num2 % i == 0) {
      gcd = i;
   }
}
Console.WriteLine ("GCD: " + gcd);
Console.WriteLine ("LCM: " + (num1 * num2) / gcd);
int GetIntegerInput (string prompt) {
   int value;
   while (true) {
      Console.Write (prompt);
      if (int.TryParse (Console.ReadLine (), out value)) {
         return value;
      } else {
         Console.WriteLine ("Invalid input. Please enter an integer.");
      }
   }
}








