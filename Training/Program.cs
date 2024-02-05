// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Program to write anagrams in a file
// --------------------------------------------------------------------------------------------
using ClassLibrary;

namespace Training {

   internal class Program {
      static void Main () {
         var filePath = "C:\\etc\\words.txt";
         string[] words = File.ReadAllLines (filePath);
         Dictionary<string, List<string>> anagramGroups = Anagrams.FindAnagrams (words);
         var outputFilePath = "C:\\etc\\anagram.txt";
         foreach (var group in anagramGroups.Values.Where (group => group.Count > 1).OrderByDescending (group => group.Count))
            Console.WriteLine ($"{group.Count} {string.Join (", ", group)}");
         Anagrams.WriteOutputToFile (anagramGroups, outputFilePath);
      }
   }
}