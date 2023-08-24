string[] wordList = File.ReadAllLines ("c:/Training/words.txt");
var charCount = new Dictionary<char, int> ();
foreach (string word in wordList) foreach (char ch in word) charCount[ch] = charCount.TryGetValue (ch, out int count) ? charCount[ch] + 1 : 1;
foreach (var kvp in charCount.OrderByDescending (entry => entry.Value).Take (7)) Console.WriteLine ($"{kvp.Key}: {kvp.Value}");