int num = GetIntegerInput ();
bool isPrime = IsPrime (num);
var res = isPrime ? $"{num} is a prime number." : $"{num} is not a prime number.";
Console.WriteLine (res);

int GetIntegerInput () {
   Console.Write ("Enter the number to check whether it's prime or not: ");
   while (true) {
      if (int.TryParse (Console.ReadLine (), out int value)) return value;
      Console.Write ("Invalid input. Please enter an integer:");
   }
}

bool IsPrime (int num) {
   if (num <= 1) return false;
   for (int i = 2; i * i <= num; i++) 
      if (num % i == 0) return false;
   return true;
}