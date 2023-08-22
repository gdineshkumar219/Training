int number = GetIntegerInput ();
for (int i = 1; i <= 10; i++) Console.Write ($"{number} * {i,2} = {number * i}\n");

int GetIntegerInput () {
   while (true) {
      Console.Write ("Enter the multiplication table you want:");
      if (int.TryParse (Console.ReadLine (), out int value) && value > 0) return value;
      Console.WriteLine ("Invalid input. Please enter an integer!!!");
   }
}