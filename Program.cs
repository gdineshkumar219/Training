Console.WriteLine ("Welcome to the Number Prediction Game!");
Console.WriteLine ("Think of a number between 1 and 128, and I will try to guess it in 7 questions.");
int guess = 0;
int lowerBound = 0;
int upperBound = 127;
int bitValue = (lowerBound + upperBound + 1) / 2;
bool correctGuess = false;
for (int questionNumber = 1; questionNumber <= 7; questionNumber++) {
   Console.WriteLine ($"Question {questionNumber}: Is the number greater than {guess + bitValue}? ((y)es/ (n)o)");
   char key = Console.ReadKey (true).KeyChar;
   if (key == 'y') {
      guess += bitValue;
      lowerBound += bitValue;
   } else if (key == 'n') {
      upperBound -= bitValue;
   } else {
      Console.WriteLine ("Invalid input. Please answer 'yes' or 'no'.");
      questionNumber--;
      bitValue = bitValue * 2;
   }
   bitValue /= 2;
   if (lowerBound == upperBound) {
      correctGuess = true;
      break;
   }
}
if (correctGuess) {
   Console.WriteLine ($"I guessed it! The number you were thinking of is {guess + 1}.");
} else {
   Console.WriteLine ("I could not guess the number correctly in 7 questions. You win!");
}