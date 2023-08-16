int rows = GetIntegerInput ("Enter the number of rows:") + 1;
for (int k = 1; k <= rows; k++) {
   PrintPatternLine (rows - k, 2 * k - 1);
}
for (int k = 1; k <= rows - 1; k++) {
   PrintPatternLine (k, 2 * (rows - k) - 1);
}void PrintPatternLine (int spaces, int stars) {
   for (int i = 0; i < spaces; i++)
      Console.Write (" ");
   for (int i = 0; i < stars; i++)
      Console.Write ("*");
   Console.WriteLine ();
}
int GetIntegerInput (string prompt) {
   int value;
   do {
      Console.Write (prompt);
   } while (!int.TryParse (Console.ReadLine (), out value));
   return value;
}