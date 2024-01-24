// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Simple Program to Print the text 'Hello, World!' in Console
// --------------------------------------------------------------------------------------------

namespace Training {
  public class ComplexNumber {
      public static (double, double) Addition ((double, double) a, (double, double) b) {
         var resR = a.Item1 + b.Item1;
         var resI = a.Item2 + b.Item2;
         return (resR, resI);
      }
      public static (double, double) GetComplexNum () {
         double a = GetInput ("Enter the input1: ");
         double b = GetInput ("Enter the input2: ");
         return (a, b);
      }
      public static double GetInput (string str) {
         while (true) {
            Console.Write (str);
            if (double.TryParse (Console.ReadLine (), out var num)) {
               return num;
            } else Console.WriteLine ("Enter valid input");
         }
      }
      public static double Norm ((double, double) x) {
         var a = x.Item1;
         var b = x.Item2;
         var res = Math.Sqrt (a * a + b * b);
         return res;
      }
      public static void Print ((double, double) x) {
         var a = x.Item1;
         var b = x.Item2;
         string res = "";
         if (a == 0) res = $"{b}i";
         if (b == 0) res = $"{a}";
         if (a < 0 && b < 0) res = $"{a}{b}i";
         if (a >= 0 && b >= 0) res = $"{a}+{b}i";
         if (a > 0 && b < 0) res = $"{a}{b}i";
         Console.WriteLine (res);
      }
      public static (double, double) Sub ((double, double) a, (double, double) b) {
         var resR = b.Item1 - a.Item1;
         var resI = b.Item2 - a.Item2;
         return (resR, resI);
      }
   }
}