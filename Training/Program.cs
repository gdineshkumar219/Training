// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Simple Program to Print the text 'Hello, World!' in Console
// --------------------------------------------------------------------------------------------

using System.Numerics;

namespace Training {
   internal class Program {
      static void Main () {
         var x = ComplexNumber.GetComplexNum ();
         var y = ComplexNumber.GetComplexNum ();
         var add= ComplexNumber.Addition (x, y);
         var sub = ComplexNumber.Sub (x, y);
         var norm = ComplexNumber.Norm (x);
         Console.Write ($"Addition:");
         ComplexNumber.Print (add);
         Console.Write ($"Subtraction:");
         ComplexNumber.Print (sub);
         Console.WriteLine ($"Norm of x: {norm}");







      }
   }
}