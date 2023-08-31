string[] wordArray = { "a", "ab", "abc", "abcdefg", "adtfiwft" };
Console.WriteLine ($"The longest Abecedarian word present is {GetLongestAbecedarian (wordArray)}.");

static string GetLongestAbecedarian (string[] wordArray) {
   if (!wordArray.Any ()) return "";
   string result = "";
   foreach (string word in wordArray) {
      char[] chars = word.ToCharArray ();
      Array.Sort (chars);
      string sortedWord = new (chars);
      if (sortedWord == word && word.Length > result.Length) result = word;
   }
   return result;
}