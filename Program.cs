int input = GetIntegerInput ();
Console.WriteLine (CalculateFactorial(input));

int GetIntegerInput () {
   while (true) {
      Console.Write ("\nEnter a number to check whether it is ARMSTRONG number or not: ");
      if (int.TryParse (Console.ReadLine (), out int value)&& value>0) return value;
      Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}

int CalculateFactorial(int n) =>Enumerable.Range (1, n).Aggregate (1, (p, item) => p * item);