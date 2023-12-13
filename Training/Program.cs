// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement double.TryParse.
// ------------------------------------------------------------------------------------------------
using ClassLibrary;

namespace Training {
   /// <summary>Contains the Main method to demonstrate the Double.TryParse class</summary>
   internal class Program {
      #region Methods -----------------------------------------------
      /// <summary>Entry Point of the program</summary>
      static void Main () {
         // Initialize a dictionary with string keys and double values
         Dictionary<string, double> expressions = new () {
            {"abc", 0}, {".34", 0}, {"45.", 0}, {".e2", 0}, {"12.", 0}, {"0", 0}, {"7", 7}, {".1", 0}, {"08.6", 8.6},
            {"7897", 7897}, {"00990.009", 990.009}, {"-7.78", -7.78}, {"-7.78e1", -77.8}, {"-7E2", -700}, {"+778e0", 778},
            {"8e-3", 0.008}, {"4e2.3", 0}, {"+4e2.3", 0}, {"0.003e", 0}, {"x", 0}, {"", 0}, {"8e9.- 3", 0},
            {"8 - .7e3", 0}, {"3.4e4 - .3", 0}, {"3.4e4 + .3", 0}, {"-35.- 354e1", 0}, {"e1", 0}, {"1e", 0},
            {"1jkse", 0}, {"1++$6e", 0}
        };
         // Print table header
         Console.WriteLine ("Input \t\t    Test Status \tParsed Value");
         Console.WriteLine ("-----------------------------------------------------");
         foreach (var kvp in expressions) {
            // Replace DoubleParser.TryParse with the actual parsing logic if necessary
            DoubleParser.TryParse (kvp.Key, out double parsedValue);
            // Determine the test status (Passed or Failed)
            Console.Write ($"{kvp.Key,-20}|");
            var msg = parsedValue == kvp.Value ? "Passed" : "Failed";
            // Set console text color based on the test status
            Console.ForegroundColor = msg == "Passed" ? ConsoleColor.Green : ConsoleColor.DarkRed;
            Console.Write ($"\t{msg,-15}");
            Console.ResetColor ();
            Console.WriteLine ($"|\t{parsedValue,-15}");
         }
      }
      #endregion
   }
}