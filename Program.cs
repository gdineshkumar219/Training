int rows = GetIntegerInput () + 1;
for (int k = 1; k <= rows; k++) PrintPatternLine (rows - k, 2 * k - 1);
for (int k = 1; k <= rows - 1; k++) PrintPatternLine (k, 2 * (rows - k) - 1);

void PrintPatternLine (int spaces, int stars) {
   Console.Write (new string (' ', spaces));
   Console.Write (new string ('*', stars));
   Console.WriteLine ();
}

int GetIntegerInput () {
   while (true) {
      Console.Write ("Enter the number of rows: ");
      if (int.TryParse (Console.ReadLine (), out int value) && value > 0) return value;
      Console.WriteLine ("Invalid input. Please enter a positive integer!!!");
   }
}