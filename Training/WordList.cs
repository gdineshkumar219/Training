namespace Training;

/// <summary>Provides functionality related to the WordList used in the Wordle game</summary>
public class WordList {
   public static readonly Random random = new ();
   public static readonly string[] puzzle = File.ReadAllLines (@"C:\etc\Puzzle-5.txt");
   public static readonly string[] dict = File.ReadAllLines (@"C:\etc\Dict-5.txt");

   /// <summary> Retrieves a random word from the puzzle array </summary>
   /// <returns>A randomly selected word from the puzzle array </returns>
   public static string RandomWord () => puzzle.ElementAt (random.Next (puzzle.Length));

   /// <summary> Checks if a given word is present in the dictionary </summary>
   public static bool IsWord (string word) => dict.Contains (word.ToUpper ());
}