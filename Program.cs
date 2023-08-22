string[] wordList = File.ReadAllLines (@"C:\Users\ganesamoorthydi\Downloads\words.txt");
var charCount = new Dictionary<char, int> ();
foreach (string word in wordList) foreach (char ch in word) if (char.IsLetterOrDigit (ch)) charCount[ch] = charCount.ContainsKey (ch) ? charCount[ch] + 1 : 1;
foreach (var kvp in charCount.OrderByDescending (entry => entry.Value).Take (7)) Console.WriteLine ($"{kvp.Key}: {kvp.Value}");