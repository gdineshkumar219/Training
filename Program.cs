int rows = GetIntegerInput ();
int val = 1, blank, i, j;
Console.WriteLine ("Pascal's triangle");
for (i = 0; i < rows; i++) {
   for (blank = 1; blank <= rows - i; blank++) Console.Write ("  ");
   for (j = 0; j <= i; j++) {
      val = j == 0 || i == 0 ? 1 : val * (i - j + 1) / j;
      Console.Write ($"{val,6}");
   }
   Console.WriteLine ();
}
int GetIntegerInput () {
   while (true) {
      Console.Write ("Enter the number of rows:");
      if (int.TryParse (Console.ReadLine (), out int value) && value > 0) return value;

      Console.WriteLine ("Invalid input. Please enter a positive integer.");
   }
}