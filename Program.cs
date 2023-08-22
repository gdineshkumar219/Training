int n = GetIntegerInput ("Enter the number of integers to find GCD and LCM: ");
int[] numbers = new int[n];
for (int i = 0; i < n; i++) numbers[i] = GetIntegerInput ("Enter number " + (i + 1) + ": ");
int gcd = CalculateGCD (numbers);
int lcm = CalculateLCM (numbers);
Console.WriteLine ("GCD: " + gcd);
Console.WriteLine ("LCM: " + lcm);

int GetIntegerInput (string prompt) {
   int value;
   while (true) {
      Console.Write (prompt);
      if (int.TryParse (Console.ReadLine (), out value) && value>=0) return value; else Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}

int CalculateGCD (int[] numbers) {
   int gcd = numbers[0];
   for (int i = 1; i < numbers.Length; i++) gcd = CalculatedGCD (gcd, numbers[i]);
      return gcd;
}

int CalculatedGCD (int a, int b) => (b == 0) ? a :CalculatedGCD (b, a % b);

int CalculateLCM (int[] numbers) {
   int lcm = numbers[0];
   for (int i = 1; i < numbers.Length; i++) lcm = (lcm * numbers[i]) / CalculatedGCD (lcm, numbers[i]);
   return lcm;
}