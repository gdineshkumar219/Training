namespace Training;

#region Guess ----------------------------------------------------------------------------------
/// <summary>
/// Represents a player's guess in the Wordle game.
/// </summary>
public class Guess {
   #region Constructor -------------------------------------------------------------------------
   /// <summary>
   /// Initializes a new instance of the <see cref="Guess"/> class with the specified word and state
   /// </summary>
   /// <param name="word">The guessed word</param>
   /// <param name="state">The array of states indicating the correctness of each letter in the guessed word</param>
   internal Guess (string word, GameStates[] state) {
      Word = word;
      State = state;
   }
   #endregion

   #region Properties --------------------------------------------------------------------------
   public string Word { get; }
   public GameStates[] State { get; }
   #endregion


}
#endregion