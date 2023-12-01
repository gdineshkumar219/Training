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
         // Array of string values to be parsed
         string[] values = {"abc",".34", "45." ,".e2","12.", "0", "7", ".1", "08.6", "7897", "00990.009",
            "-7.78", "-7.78e1", "-7E2", "+778e0", "8e-3", "4e2.3", "+4e2.3", "0.003e","x", "","8e9.-3",
             "8-.7e3", "3.4e4-.3", "3.4e4+.3", "-35.-354e1", "e1", "1e", "1jkse", "1++$6e" };
         Console.WriteLine ("-------------------Custom double.TryParse-------------------");
         // Call method to print results using custom TryParse
         PrintResults (values, "Custom");
         Console.WriteLine ("\n------------------Built-in double.TryParse------------------");
         // Call method to print results using built-in TryParse
         PrintResults (values, "Built-in");
      }

      /// <summary>Method to print results</summary>
      /// <param name="values">Array of string values to be parsed</param>
      /// <param name="methodName">Method name, either "Custom" or "Built-in"</param>
      static void PrintResults (IEnumerable<string> values, string methodName) {
         Console.WriteLine ($"Parsed status    | {"Input",-15} | {"Output",15}");
         Console.WriteLine (new string ('-', 60));
         foreach (var val in values) {
            bool success = false;
            double res = 0;
            // Check if the method is "Custom" and attempt to parse using custom TryParse
            if (methodName.Equals ("Custom")) success = DoubleParser.TryParse (val, out res);
            // Check if the method is "Built-in" and attempt to parse using built-in TryParse
            if (methodName.Equals ("Built-in")) success = double.TryParse (val, out res);
            // Set console text color based on parsing success
            Console.ForegroundColor = success ? ConsoleColor.Green : ConsoleColor.DarkRed;
            // Print whether the value was parsed successfully or not
            Console.Write ($"{(success ? "Pass" : "Fail"),-15} ");
            // Reset console text color
            Console.ResetColor ();
            // Print the parsing result message
            Console.WriteLine ($" | {val,-15} | {res}");
         }
         Console.WriteLine (new string ('-', 60));
      }
      #endregion
   }
}