namespace Training;

public class WordList {
   public static readonly Random random = new ();
   public static readonly string[] puzzle = File.ReadAllLines (@"C:\etc\puzzle.txt");
   public static readonly string[] dict = File.ReadAllLines (@"C:\etc\dict.txt");
   public static string RandomWord () => puzzle.ElementAt (random.Next (puzzle.Length));
   public static bool IsWord (string word) => dict.Contains (word.ToUpper ());
}