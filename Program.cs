Console.WriteLine ("Welcome to the Number Prediction Game!");
Console.WriteLine ("Think of a number between 0 and 127, and I will try to guess it in 7 questions.");
int N = 2, rem = 0, pow = 1;
int result = 0;
for (int i = 1; i <= 7; i++) {
   Console.WriteLine ($"Question {i}: Is the remainder {rem} when divided by{N}? ((y)es/ (n)o)");
   char key = Console.ReadKey (true).KeyChar;
   if (key == 'y') {
      result += (int)(0 * Math.Pow (2, pow));
   } else {
      rem = rem + N / 2;
      result += (int)(1 * Math.Pow (2, pow));
   }
   pow++; N = (int)(Math.Pow (2, i + 1));
}
Console.WriteLine ($"I guessed it! The number you were thinking of is {rem}.");
