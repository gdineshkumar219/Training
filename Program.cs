int num;
while (true) {
   Console.Write ("Enter the number of terms: ");
   if (int.TryParse (Console.ReadLine (), out num) && num >= 0) break;
   else Console.WriteLine ("Invalid input. Please enter a non-negative integer.");
}
Console.WriteLine ("Fibonacci series is:");
for (int i = 0; i < num; i++) Console.WriteLine ($"{FibonacciNumber (i)}");

int FibonacciNumber (int num) => num <= 1 ? num : FibonacciNumber (num - 1) + FibonacciNumber (num - 2);