int input = GetIntegerInput ();
Console.WriteLine ($"{input}! is {CalculateFactorial (input)}");

int GetIntegerInput () {
   while (true) {
      Console.Write ("Enter a number to find the factorial: ");
      if (int.TryParse (Console.ReadLine (), out int value) && value > 0) return value;
      Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}

int CalculateFactorial (int n) => Enumerable.Range (1, n).Aggregate ((p, item) => p * item);