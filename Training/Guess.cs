namespace Training;

public class Guess {
   public string Word { get; }
   public GameStates[] State { get; }

   internal Guess (string word, GameStates[] state) {
      Word = word;
      State = state;
   }
}