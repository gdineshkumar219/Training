int num = GetIntegerInput ("Enter the number to check whether it's prime or not: ");
bool isPrime = IsPrime (num);
if (isPrime) {
   Console.WriteLine ($"{num} is a prime number.");
} else {
   Console.WriteLine ($"{num} is not a prime number.");
}
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
bool IsPrime (int num) {
   if (num <= 1) {
      return false;
   }
   for (int i = 2; i * i <= num; i++) {
      if (num % i == 0) {
         return false;
      }
   }
   return true;
}