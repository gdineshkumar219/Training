int n = GetIntegerInput ("Enter the number of integers to find GCD and LCM: ");
int[] numbers = new int[n];
for (int i = 0; i < n; i++) numbers[i] = GetIntegerInput ($"Enter number{i + 1}: ");
int gcd = InputForGCD (numbers);
int lcm = CalculateLCM (numbers);
Console.WriteLine ($"GCD:{gcd}");
Console.WriteLine ($"LCM: {lcm}");

int GetIntegerInput (string prompt) {
   while (true) {
      Console.Write (prompt);
      if (int.TryParse (Console.ReadLine (), out int value) && value >= 0) return value;
      else Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}

int InputForGCD (int[] numbers) {
   int gcd = numbers[0];
   for (int i = 1; i < numbers.Length; i++) gcd = numbers[i] == 0 ? gcd : CalculateGCD (gcd, numbers[i]);
   return gcd;
}

int CalculateGCD (int a, int b) => (b == 0) ? a : CalculateGCD (b, a % b);

int CalculateLCM (int[] numbers) {
   int lcm = numbers[0];
   for (int i = 1; i < numbers.Length; i++) lcm = (lcm * numbers[i]) / CalculateGCD (lcm, numbers[i]);
   return lcm;
}