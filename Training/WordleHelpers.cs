namespace Training;

# region WordleHelpers -------------------------------------------------------------------------
/// <summary> Provides helper methods for the Wordle game</summary>
public class WordleHelpers {
   #region Methods -----------------------------------------------------------------------------
   /// <summary>Maps a ConsoleColor to the corresponding GameStates </summary>
   /// <param name="color">The ConsoleColor to be mapped</param>
   /// <returns>The GameStates corresponding to the ConsoleColor</returns>
   public static GameStates State (ConsoleColor color) => color switch {
      ConsoleColor.DarkGray => GameStates.ABSENT,
      ConsoleColor.Blue => GameStates.PRESENT,
      ConsoleColor.Green => GameStates.CORRECT,
      _ => GameStates.NONE
   };

   /// <summary>Maps a GameStates to the corresponding ConsoleColor </summary>
   /// <param name="state">The GameStates to be mapped</param>
   /// <returns>The ConsoleColor corresponding to the GameStates</returns>
   public static ConsoleColor StateColor (GameStates state) => state switch {
      GameStates.ABSENT => ConsoleColor.DarkGray,
      GameStates.PRESENT => ConsoleColor.Blue,
      GameStates.CORRECT => ConsoleColor.Green,
      _ => Console.ForegroundColor
   };
   #endregion
}
#endregion