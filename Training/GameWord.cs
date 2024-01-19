namespace Training;

public class GameWord {
   public string Word { get; }
   public Dictionary<char, int> UniqueCharCnt { get; }

   internal GameWord (string word) {
      Word = word;
      UniqueCharCnt = new Dictionary<char, int> (word.Length);
      foreach (char letter in word) {
         if (UniqueCharCnt.TryGetValue (letter, out int value))
            UniqueCharCnt[letter] = ++value;
         else UniqueCharCnt.Add (letter, 1);
      }
   }
}