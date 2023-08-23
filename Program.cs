char[] seed = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' };
string[] wordList = File.ReadAllLines (@"C:\Users\ganesamoorthydi\Downloads\words.txt");
int total = 0;
List<(string word, int score)> validWordScores = new List<(string, int)> ();
foreach (var word in wordList) {
   if (word.Length >= 4 && word.Contains (seed[0]) && word.All (seed.Contains)) {
      int score = seed.All (word.Contains) ? word.Length + 7 : (word.Length == 4 ? 1 : word.Length);
      total += score;
      validWordScores.Add ((word, score));
   }
}
validWordScores.Sort ((a, b) => b.score.CompareTo (a.score));
foreach (var (word, score) in validWordScores) {
   Console.ForegroundColor = seed.All (word.Contains)  ? ConsoleColor.Green :ConsoleColor.White;
   Console.WriteLine ($"{score,4}. {word}");
}
Console.ResetColor ();
Console.WriteLine ("----");
Console.WriteLine ($" {total} Total Score");