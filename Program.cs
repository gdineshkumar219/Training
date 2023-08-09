Console.WriteLine ("Welcome to the Number Prediction Game!");
Console.WriteLine ("Think of a number between 0 and 127, and I will try to guess it in 7 questions.");
int rem = 0;
for (int i = 1; i <= 7; i++) {
   Console.WriteLine ($"Question {i}: Is the remainder {rem} when divided by {Math.Pow (2, i)}? ((y)es / (n)o)");
   rem += (Console.ReadKey (true).Key == ConsoleKey.N) ? (int)Math.Pow (2, (i - 1)) : 0;
}
Console.WriteLine ($"I guessed it! The number you were thinking of is {rem}.");


