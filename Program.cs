int a = GetIntegerInput ();
int b = GetIntegerInput ();
Console.WriteLine ("The values before swapping:");
Console.WriteLine ($"a={a}\nb={b}");
SwapTwoValues (ref a, ref b);
Console.WriteLine ("The values after swapping: ");
Console.WriteLine ($"a={a} \nb={b}");

int GetIntegerInput () {
   while (true) {
      Console.Write ("Enter a numbers to Swap: ");
      if (int.TryParse (Console.ReadLine (), out int value) && value > 0) return value;
      Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}

void SwapTwoValues (ref int a, ref int b) => (a, b) = (b, a);