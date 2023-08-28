string words = File.ReadAllText ("C:/etc/words.txt") ;
Dictionary<char, int> charCount = new();
foreach (char ch in words) if (char.IsLetter(ch))charCount[ch] = charCount[ch] = charCount.GetValueOrDefault (ch) + 1; ;
foreach (var kvp in charCount.OrderByDescending (entry => entry.Value).Take (7)) Console.WriteLine ($"{kvp.Key}: {kvp.Value}");