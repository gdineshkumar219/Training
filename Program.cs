int num;
while (true) {
   Console.Write ("Enter the number of terms: ");
   if (int.TryParse (Console.ReadLine (), out num) && num >= 0) {
      break;
   } else {
      Console.WriteLine ("Invalid input. Please enter a non-negative integer.");
   }
}
Console.WriteLine ("Fibonacci series is:");
for (int i = 0; i < num; i++) {
   Console.WriteLine ($"{FibonacciSeries (i)}");
}
int FibonacciSeries (int num) {
   return num <= 1 ? num : FibonacciSeries (num - 1) + FibonacciSeries (num - 2);
}