int number = GetIntegerInput ();
Console.WriteLine ($"The digital root value of {number} is {CalculateDigitalRoot (number)}");

int GetIntegerInput () {
   while (true) {
      Console.Write ("Enter the number to find the digital root:");
      if (int.TryParse (Console.ReadLine (), out int value) && value > 0) {
         return value;
      }
      Console.WriteLine ("Invalid input. Please enter a positive integer.");
   }
}

int CalculateDigitalRoot (int n) {
   while (n > 9) {
      int sum = 0;
      while (n > 0) {
         sum += n % 10;
         n /= 10;
      }
      n = sum;
   }
   return n;
}