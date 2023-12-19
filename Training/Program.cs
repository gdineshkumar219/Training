// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Simple Program to Print the text 'Hello, World!' in Console
// --------------------------------------------------------------------------------------------
using ClassLibrary;

namespace Training {
   internal class Program {
      static void Main () {
         string input = Console.ReadLine ().ToUpper () + '~';
         Console.WriteLine (FileNameParser.FileNameParse (input));
      }
   }
}