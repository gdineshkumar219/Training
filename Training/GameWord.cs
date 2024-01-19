namespace Training;

/// <summary>
/// Represents a word in the Wordle game, providing information about the word and its unique character count
/// </summary>
public class GameWord {
   public string Word { get; }
   public Dictionary<char, int> UniqueCharCnt { get; }

   /// <summary>
   /// Initializes a new instance of the <see cref="GameWord"/> class with the specified word
   /// </summary>
   /// <param name="word">The word to be represented in the Wordle game.</param>
   public GameWord (string word) {
      Word = word;
      UniqueCharCnt = new Dictionary<char, int> (word.Length);
      foreach (char letter in word) {
         if (UniqueCharCnt.TryGetValue (letter, out int value))
            UniqueCharCnt[letter] = ++value;
         else UniqueCharCnt.Add (letter, 1);
      }
   }
}