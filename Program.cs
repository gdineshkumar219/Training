Console.WriteLine ("I'm thinking of a number between 1 and 100.");
Console.WriteLine ("You have to guess it.");
int targetNumber = new Random ().Next (1, 101);
for (int i=1; ; i++) {
   Console.Write ("Enter your guess:");
   string value = Console.ReadLine ();
   if (!int.TryParse (value, out int result) || (result < 1 || result > 100)) { Console.WriteLine ("Please enter a value between 1 and 100");  }
   if (result == targetNumber) {
      Console.WriteLine ($"Your guess is correct at {i} tries.");
      Console.WriteLine (result > targetNumber ? "Your guess is too high" : "Your guess is too small");
      if (!PlayAgain ()) break;
      else { targetNumber = new Random ().Next (1, 101); i = 0; }
   } else Console.WriteLine (result > targetNumber ? "Your guess is too high" : "Your guess is too small");
}
bool PlayAgain () {
   Console.WriteLine ("Do you want to play again? (Y)es / (N)o");
   char key = Console.ReadKey (true).KeyChar;
   //char response = Console.ReadKey (true).KeyChar;
   return key == 'y';
}

