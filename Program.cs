int number = GetIntegerInput ("Enter the multiplication table you want:");
for (int i = 1; i <= 10; i++) {
   Console.Write ($"{number} * {i,2} = {number * i}\n");
}
int GetIntegerInput (string prompt) {
   while (true) {
      Console.Write (prompt);
      if (int.TryParse (Console.ReadLine (), out int value)) return value;
      Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}