int rows = GetIntegerInput ("Enter the number of rows:");
int val = 1, blank, i, j;
Console.WriteLine ("Pascal's triangle");
for (i = 0; i < rows; i++) {
   for (blank = 1; blank <= rows - i; blank++)
      Console.Write (" ");
   for (j = 0; j <= i; j++) {
      if (j == 0 || i == 0)
         val = 1;
      else
         val = val * (i - j + 1) / j;
      Console.Write (val + " ");
   }
   Console.WriteLine ();
}
int GetIntegerInput (string prompt) {
   while (true) {
      Console.Write (prompt);
      if (int.TryParse (Console.ReadLine (), out int value) && value > 0) {
         return value;
      }
      Console.WriteLine ("Invalid input. Please enter a positive integer.");
   }
}