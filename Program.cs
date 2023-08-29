string[] wordArray = { "a", "ab", "abc", "abcdef", "adtfiwft" };
Console.WriteLine (ToFindLongestAbecedarian (wordArray));

static string ToFindLongestAbecedarian (string[] wordArray) {
   if (!wordArray.Any ()) return "Empty array!";
   string result = "";
   foreach (string word in wordArray) {
      char[] chars = word.ToCharArray ();
      Array.Sort (chars);
      string sortedWord = new (chars);
      if (sortedWord == word && word.Length > result.Length) result = word;
   }
   return result;
}