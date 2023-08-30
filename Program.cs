int a = GetIntegerInput ();
int b = GetIntegerInput ();
Console.WriteLine ("The values before swapping:");
Console.WriteLine ($"a={a}");
Console.WriteLine ($"b={b}");
SwapTwoValues (a, b);

int GetIntegerInput () {
   while (true) {
      Console.Write ("Enter a numbers to Swap: ");
      if (int.TryParse (Console.ReadLine (), out int value) && value > 0) return value;
      Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}
void SwapTwoValues (int a, int b) {
   (a, b) = (b, a);
   Console.WriteLine ("The values after swapping: ");
   Console.WriteLine ($"a={a}");
   Console.WriteLine ($"b={b}");
}