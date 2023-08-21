string[] wordList = File.ReadAllLines (@"C:\Users\ganesamoorthydi\Downloads\words.txt");
Dictionary<char, int> charCount = new Dictionary<char, int> ();
foreach (string word in wordList) {
   foreach (char ch in word) {
      if (char.IsLetterOrDigit (ch)) {
         var c= charCount.ContainsKey (ch) ? charCount[ch]++ : charCount[ch]=1;
         }
      }
   }
var sortedCharCount = from entry in charCount orderby entry.Value descending select entry;
foreach (var kvp in sortedCharCount.Take (7)) { 
   Console.WriteLine ($"{kvp.Key}: {kvp.Value}");
}