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
         Anagrams.WriteOutputToFile (anagramGroups, outputFilePath);
      }
   }
}