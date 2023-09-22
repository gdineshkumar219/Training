namespace Training {
   /// <summary>Contains methods for vote counting</summary>
   internal class Program {
      /// <summary>The entry point of the program</summary>
      static void Main () {
         Console.WriteLine ("Vote counting");
         string input = GetInput ("Enter the input string: ");
         var winner = FindWinner (input);
         Console.WriteLine ($"{winner.Item1} won by getting {winner.Item2} votes");
      }

      /// <summary>Prompts the user for a string input containing letters only</summary>
      /// <param name="prompt">The prompt message displayed to the user</param>
      /// <returns>The valid input string containing letters only</returns>
      static string GetInput (string prompt) {
         string input;
         while (true) {
            Console.Write ($"{prompt}");
            input = Console.ReadLine ();
            if (input.All (char.IsLetter)) return input;
            Console.WriteLine ("Input should contain only letters (no digits). Please try again.");
         }
      }

      /// <summary>Finds the winner character in the input string</summary>
      /// <param name="input">The input string to analyze</param>
      /// <returns>A tuple containing the winner character and the number of votes</returns>
      static (char, int) FindWinner (string input) {
         var voteCount = new Dictionary<char, int> ();
         foreach (char ch in input.ToLower ()) voteCount[ch] = voteCount.TryGetValue (ch, out int value) ? value + 1 : 1;
         int maxVotes = voteCount.Values.Max ();
         var kv = voteCount.MaxBy (a => a.Value);
         return (kv.Key, kv.Value);
      }
   }
}