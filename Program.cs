int number = GetIntegerInput ("Enter the number to check whether it is palindrome or not:");
int originalNumber = number;
int reversedNumber = 0;
while (number != 0) {
   int digit = number % 10;
   reversedNumber = reversedNumber * 10 + digit;
   number /= 10;
}
Console.WriteLine ($"The reverse of {originalNumber} is {reversedNumber}.");
var res = originalNumber == reversedNumber ? $"{originalNumber} is a palindrome." : $"{originalNumber} is not a palindrome.";
Console.WriteLine (res);
int GetIntegerInput (string prompt) {
   while (true) {
      Console.Write (prompt);
      if (int.TryParse (Console.ReadLine (), out int value) && value > 0) {
         return value;
      }
      Console.WriteLine ("Invalid input. Please enter a positive integer.");
   }
}