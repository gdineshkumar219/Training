// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Program class for testing FileNameParser
// --------------------------------------------------------------------------------------------
using ClassLibrary;

namespace Training {
   /// <summary> Program class for testing FileNameParser</summary>
   internal class Program {
      #region Methods -----------------------------------------------
      /// <summary> Entry point of the program</summary>
      static void Main () {
         var testPaths = new string[]
         {
            @"Cz:\abc\def\r.txt", @"C:\abc\def\r.txt", @"C:\Readme.txt", @"C:\abc\.bcf",
            @"C:\abc\bcf.", @"Readme.txt", @"C:\abc\def", @"C:\abc d", @"\abcd\Readme.txt", " ",
            @"C:\ab.c\def\r.txt", @"C:\abc:d", @".\abc", ".abc", "abc", @"C:\abc6\def\r.txt",
            @"C:\abc\def\r.txt.txt", @"C:\work\r.txt"
         };
         Console.WriteLine ("{0,-40} {1,-10} {2,-20} {3,-20} {4,-10} {5,-10}", "Input", "Drive", "Folder", "Filename", "Extension", "Result");
         Console.WriteLine ("".PadRight (110, '-'));
         foreach (var path in testPaths) DisplayResult (path);
         // Continuous input from the user until 'exit' is entered
         while (true) {
            Console.WriteLine ("\nEnter file path or type 'exit' to end ");
            Console.Write (">");
            string input = Console.ReadLine ().ToLower ();
            if (input.Equals ("exit")) break;
            DisplayResult (input);
         }
      }

      /// <summary>Display the parsing result for a given file path</summary>
      /// <param name="path">The file path to parse</param>
      static void DisplayResult (string path) {
         (string dLetter, string folder, string flName, string ext) result;
         bool sucess = FileNameParser.FileNameParse (path, out result);
         Console.Write ($"{path,-40} ");
         if (sucess) {
            Console.Write ($"{result.dLetter,-10}|{result.folder,-20}|{result.flName,-20}|{result.ext,-10}|");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ("Passed");
         } else {
            Console.Write ($"{"-",-10}|{"-",-20}|{"-",-20}|{"-",-10}|");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("Failed");
         }
         Console.ResetColor ();
      }
      #endregion
   }
}