using System.Reflection;

namespace Training {
   /// <summary>Contains methods for vote counting.</summary>
   internal class Program {
      /// <summary>The entry point of the program.</summary>
      static void Main () {
         Console.WriteLine ("Vote counting");
         string prompt = "Enter the input string: ";
         string input = GetStringInput (prompt);
         FindWinner (input, out string res);
         Console.WriteLine ($"The winner is {res}");
      }

      /// <summary>Prompts the user for a string input containing letters only.</summary>
      /// <param name="prompt">The prompt message displayed to the user.</param>
      /// <returns>The valid input string containing letters only.</returns>
      static string GetStringInput (string prompt) {
         string input;
         while (true) {
            Console.Write ($"{prompt}");
            input = Console.ReadLine ();
            if (input.All (char.IsLetter)) return input;
            Console.WriteLine ("Input should contain only characters (no digits). Please try again.");
         }
      }

      /// <summary>Finds the winner character in the input string.</summary>
      /// <param name="input">The input string to analyze.</param>
      /// <param name="caseInsensitive">Determines whether the comparison is case-insensitive.It is an optional parameter.</param>
      /// <returns>The winning character (either lowercase or uppercase).</returns>
      static void FindWinner (string input, out string res, bool caseInsensitive = false) {
         if (!caseInsensitive) input = input.ToLower ();
         Dictionary<char, int> voteCount = new Dictionary<char, int> ();
         foreach (char ch in input) voteCount[ch] = voteCount.ContainsKey (ch) ? voteCount[ch] + 1 : 1;
         int maxVotes = voteCount.Values.Max ();
         char winner = voteCount.First (kv => kv.Value == maxVotes).Key;
         res = $"{char.ToUpper (winner)} or {winner}";
      }
   }
}