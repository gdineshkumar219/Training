namespace Training;
public enum GameStates {
   NONE,
   ABSENT,
   PRESENT,
   CORRECT
}

public class WordleHelpers {
   public static GameStates State (ConsoleColor color) => color switch {
      ConsoleColor.DarkGray => GameStates.ABSENT,
      ConsoleColor.Blue => GameStates.PRESENT,
      ConsoleColor.Green => GameStates.CORRECT,
      _ => GameStates.NONE
   };
   public static ConsoleColor StateColour (GameStates state) => state switch {
      GameStates.ABSENT => ConsoleColor.DarkGray,
      GameStates.PRESENT => ConsoleColor.Blue,
      GameStates.CORRECT => ConsoleColor.Green,
      _ => Console.ForegroundColor
   };
}