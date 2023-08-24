string[] wordList = File.ReadAllLines ("C:\\etc\\words.txt") ;
Dictionary < char, int>charCount = new Dictionary <char, int> ();
foreach (string word in wordList) foreach (char ch in word) charCount[ch] = charCount.TryGetValue (ch, out int count) ? count + 1 : 1;
foreach (var kvp in charCount.OrderByDescending (entry => entry.Value).Take (7)) Console.WriteLine ($"{kvp.Key}: {kvp.Value}");