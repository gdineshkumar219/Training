// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Simple Program to Print the text 'Hello, World!' in Console
// --------------------------------------------------------------------------------------------
using Training;
#region class Program --------------------------------------------------------------------------
class Program {
   #region Method ------------------------------------------------
   static void Main () {
      // Create an instance of the Evaluator class for mathematical expression evaluation
      var eval = new Evaluator ();
      // Infinite loop for user input
      for (; ; ) {
         // Prompt for user input
         Console.Write ("> ");
         // Read and trim the user input
         string text = Console.ReadLine ().Trim ().ToLower ();
         // Exit the loop if the user inputs "exit"
         if (text == "exit") break;
         try {
            // Evaluate the user input and display the result
            double result = eval.Evaluate (text);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine (result);
         } catch (EvalException e) {
            // Display any exception messages in case of an error
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine (e.Message);
         } finally {
            // Reset console color after displaying the result or error
            Console.ResetColor ();
         }
      }
   }
   #endregion
}

#endregion
