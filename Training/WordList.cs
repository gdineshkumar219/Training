namespace Training;

#region WordList -------------------------------------------------------------------------------
/// <summary>Provides functionality related to the WordList used in the Wordle game</summary>
public class WordList {
   #region Methods -----------------------------------------------------------------------------
   /// <summary> Retrieves a random word from the puzzle array </summary>
   /// <returns>A randomly selected word from the puzzle array </returns>
   public static string RandomWord () => mPuzzle.ElementAt (mRandom.Next (mPuzzle.Length));

   /// <summary> Checks if a given word is present in the dictionary </summary>
   public static bool IsWord (string word) => mDict.Contains (word.ToUpper ());
   #endregion

   #region fields ------------------------------------------------------------------------------
   static readonly Random mRandom = new ();
   static readonly string[] mPuzzle = File.ReadAllLines (@"C:\etc\Puzzle-5.txt");
   static readonly string[] mDict = File.ReadAllLines (@"C:\etc\Dict-5.txt");
   #endregion
}
#endregion