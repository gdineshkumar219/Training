char[] seed = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' };
string[] wordList = File.ReadAllLines (@"C:\Users\ganesamoorthydi\Downloads\words.txt");
int total = 0;
string[] validWords = new string[wordList.Length]; int validWordIndex = 0;
int[] scores = new int[wordList.Length];
int score;
foreach (var word in wordList) {
   if (word.Length >= 4 && word.Contains (seed[0]) && (word.All (seed.Contains))) {
      score = seed.All (word.Contains) ? word.Length + 7 : (word.Length == 4 ? 1 : word.Length);
      total += score;
      validWords[validWordIndex] = word; scores[validWordIndex] = score;
      validWordIndex++;
   }
}
Array.Sort (scores, validWords, 0, validWordIndex, Comparer<int>.Create ((a, b) => b.CompareTo (a)));
for (int i = 0; i < validWordIndex; i++) {
   if (scores[i] == 15) Console.ForegroundColor = ConsoleColor.Green;
   Console.WriteLine ($"{scores[i],4}. {validWords[i]}");
   Console.ResetColor ();
}
Console.WriteLine ("----");
Console.WriteLine ($" {total} Total Score");