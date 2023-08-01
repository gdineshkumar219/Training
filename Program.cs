char[] letters = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' }; // Define the seed letters for the Spelling Bee puzzle
string[] wordList = File.ReadAllLines (@"C:\New folder\words.txt");// Read the word list from a file into an array 
List<(string word, int score)>validWords = new List<(string word, int score)>();//List to store the valid words along with score
int total = 0;//Variable to score the total score of the valid words
foreach (var word in wordList) {
   // Check if the word meets the Spelling Bee rules: at least 4 letters long, contains 'U', and only uses the seed letters
   if (word.Length >= 4 && word.Contains ('U') && (word.All (letters.Contains))) {
      int score;
      if (letters.All (letter => word.Contains (letter))) {
         score = word.Length + 7; // Bonus points for pangram
      } else {
         score = word.Length == 4 ? 1 : word.Length;
         Console.ResetColor ();
      }
      total += score;
      validWords.Add((word, score));
   }
}
Console.ResetColor();
validWords.Sort ((a, b) => b.score.CompareTo (a.score));
foreach(var (word,score) in validWords) {
   if (score == 15) 
      Console.ForegroundColor = ConsoleColor.Green;Console.WriteLine ($"{score,4}. {word}");Console.ResetColor ();
}
Console.WriteLine ("----");
Console.WriteLine ($" {total} Total Score");






