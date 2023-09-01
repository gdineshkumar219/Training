string[] wordArray = { "ab", "pqrstuvw", "abd", "defg", "jrvgnk" };
Console.WriteLine ($"The longest Abecedarian word present is {GetLongestAbecedarian (wordArray)}.");

static string GetLongestAbecedarian (string[] wordArray) {
   string result = "";
   if (wordArray == null || !wordArray.Any ()) return result;
   foreach (string word in wordArray) {
      char[] chars = word.ToCharArray ();
      Array.Sort (chars);
      string sortedWord = new (chars);
      if (sortedWord == word && word.Length > result.Length) result = word;
   }
   return result;
}