Console.WriteLine ("I'm thinking of a number between 1 and 100.");
Console.WriteLine ("You have to guess it.");
do {
   int targetNumber = new Random ().Next (1, 101);
   for (int i = 1; ; i++) {
      Console.Write ("Enter your guess:");
      string value = Console.ReadLine ();
      if (!int.TryParse (value, out int result) || (result < 1 || result > 100)) { Console.WriteLine ("Please enter a value between 1 and 100"); continue; }
      if (result == targetNumber) {
         Console.WriteLine ($"Your guess is correct at {i} tries."); break;
      }
      Console.WriteLine (result > targetNumber ? "Your guess is too high" : "Your guess is too small");
   }
}
while (PlayAgain ());
bool PlayAgain () {
   Console.WriteLine ("Do you want to play again? (y)es / (n)o");
   string response = Console.ReadLine ();
   return response == "y" || response == "Y";
}

