char[] letters = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' }; // Define the seed letters for the Spelling Bee puzzle
string[] wordList = File.ReadAllLines (@"C:\New folder\words.txt");// Read the word list from a file into an array 
int total = 0;//Variable to score the total score of the valid words
string[] validWords = new string[wordList.Length]; int validWordIndex = 0;
int[] scores = new int[wordList.Length];
foreach (var word in wordList) {
   // Check if the word meets the Spelling Bee rules: at least 4 letters long, contains 'U', and only uses the seed letters
   if (word.Length >= 4 && word.Contains ('U') && (word.All (letters.Contains))) {
      int score;
      if (letters.All (letter => word.Contains (letter))) {
         score = word.Length + 7; // Bonus points for pangram
      } else {
         score = word.Length == 4 ? 1 : word.Length;
      }
      total += score;
      validWords[validWordIndex] = word;
      scores[validWordIndex] = score;
      validWordIndex++;
      //validWords.Add((word, score));
   }
}
// Sort the arrays of valid words and scores in descending order based on scores
Array.Sort (scores, validWords, 0, validWordIndex, Comparer<int>.Create ((a, b) => b.CompareTo (a)));
// Print the formatted output
for (int i = 0; i < validWordIndex; i++) {
   if (scores[i] == 15) { Console.ForegroundColor = ConsoleColor.Green; }

   Console.WriteLine ($"{scores[i],4}. {validWords[i]}");
   Console.ResetColor ();
}
Console.WriteLine ("----");
// Print the total score of valid words

Console.WriteLine ($"{total} Total Score");

