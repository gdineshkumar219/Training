// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Classlib.cs
// ---------------------------------------------------------------------------------------------
namespace ClassLibrary {
   /// <summary>Provides methods for finding anagrams and writing the results to a file </summary>
   public class Anagrams {
      /// <summary> Writes anagrams to the specified output file </summary>
      /// <param name="anagramGroups">A dictionary containing anagram groups</param>
      /// <param name="opFilePath">The path to the output file</param>
      public static void WriteOutputToFile (Dictionary<string, List<string>> anagramGroups, string opFilePath) {
         try {
            using (StreamWriter sw = new (opFilePath))
               foreach (var group in anagramGroups.Values)
                  sw.WriteLine ($"{group.Count} {string.Join (", ", group.OrderByDescending (w => w.Length))}");
            Console.WriteLine ($"Output written to {opFilePath}");
         } catch (Exception e) {
            Console.WriteLine ($"An error occurred while writing to the file: {e.Message}");
         }
      }

      /// <summary>Finds anagrams in an array of words</summary>
      /// <param name="words">An array of words</param>
      /// <returns>A dictionary containing anagram groups</returns>
      public static Dictionary<string, List<string>> FindAnagrams (string[] words) {
         Dictionary<string, List<string>> anagramGroups = new ();
         foreach (var word in words) {
            string sortedWord = new (word.OrderBy (c => c).ToArray ());
            if (!anagramGroups.TryGetValue (sortedWord, out var group)) {
               group = new List<string> ();
               anagramGroups[sortedWord] = group;
            }
            group.Add (word);
         }
         return anagramGroups;
      }
   }
}